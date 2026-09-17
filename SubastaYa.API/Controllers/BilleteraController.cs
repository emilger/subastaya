using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubastaYa.API.Data;
using SubastaYa.API.DTOs;
using SubastaYa.API.Models;

namespace SubastaYa.API.Controllers
{
    // Controller para manejar operaciones relacionadas con la billetera del usuario.
    [ApiController]
    [Route("api/v1/wallets")]
    public class BilleteraController : ControllerBase
    {
        // Inyección del contexto de la base de datos para acceder a las billeteras y transacciones.
        private readonly AplicationDbContext _context;

        public BilleteraController(AplicationDbContext context)
        {
            _context = context;
        }

        // Muestra cuánto dinero tiene guardado un usuario
        [HttpGet("{usuarioId}")]
        public async Task<ActionResult<BilleteraResponseDto>> ObtenerBilletera(int usuarioId)
        {
            var billetera = await _context.Billeteras
                .FirstOrDefaultAsync(b => b.UsuarioId == usuarioId);

            if (billetera == null)
            {
                return NotFound(new { mensaje = $"No se encontró billetera para el usuario con ID {usuarioId}." });
            }

            var response = new BilleteraResponseDto
            {
                BilleteraId = billetera.BilleteraId,
                UsuarioId = billetera.UsuarioId,
                SaldoTotal = billetera.SaldoTotal,
                SaldoRetenido = billetera.SaldoRetenido,
                SaldoDisponible = billetera.SaldoDisponible,
                Version = billetera.Version
            };

            return Ok(response);
        }

        //Le agrega dinero a la billetera de un usuario y guarda el comprobante de la recarga
        [HttpPost("cargar/{usuarioId}")]
        public async Task<ActionResult<BilleteraResponseDto>> CargarSaldo(int usuarioId, [FromBody] CargarSaldoDto dto)
        {
            if (dto.Monto <= 0)
                return BadRequest(new { mensaje = "El monto a depositar debe ser mayor a cero." });

            using var transaccion = await _context.Database.BeginTransactionAsync();
            try
            {

                var billetera = await _context.Billeteras
                    .FirstOrDefaultAsync(b => b.UsuarioId == usuarioId);

                if (billetera == null)
                    return NotFound(new { mensaje = $"No se encontró la billetera para el usuario {usuarioId}." });

                // Acreditar SaldoTotal (SaldoDisponible se auto-calcula en PostgreSQL)
                billetera.SaldoTotal += dto.Monto;
                billetera.Version++;

                _context.TransaccionesLedger.Add(new TransaccionLedger
                {
                    BilleteraId = billetera.BilleteraId,
                    TipoTransaccion = "DEPOSITO",
                    Monto = dto.Monto,
                    Fecha = DateTime.UtcNow,
                    SubastaId = null
                });

                _context.Auditorias.Add(new Auditoria_Log
                {
                    Entidad = "BILLETERA",
                    EntidadId = billetera.BilleteraId,
                    Accion = "ACREDITACION_MANUAL_SALDO",
                    UsuarioId = usuarioId,
                    Detalle_Json = $"{{\"monto\": {dto.Monto}, \"nuevoSaldoTotal\": {billetera.SaldoTotal}}}",
                    Fecha = DateTime.UtcNow
                });
                 // Persistir en la base de datos primero
                    await _context.SaveChangesAsync();
                    await transaccion.CommitAsync();

                // Mapear el DTO
                var response = new BilleteraResponseDto
                {
                    BilleteraId = billetera.BilleteraId,
                    UsuarioId = billetera.UsuarioId,
                    SaldoTotal = billetera.SaldoTotal,
                    SaldoRetenido = billetera.SaldoRetenido,
                    SaldoDisponible = billetera.SaldoDisponible,
                    Version = billetera.Version
                }; 
                // Devolver la respuesta 
                return Ok(new
                {
                    mensaje = "Depósito realizado con éxito.",
                    billetera = response
                });
            }
            catch (DbUpdateConcurrencyException)
            {
                await transaccion.RollbackAsync();
                return Conflict(new { mensaje = "Conflicto de concurrencia al actualizar la billetera." });
            }
            catch (Exception ex)
            {
                await transaccion.RollbackAsync();
                return StatusCode(500, new { mensaje = "Error interno.", detalle = ex.Message });
            }
        }

        [HttpGet("users/{usuarioId}/movements")]
            public async Task<ActionResult<IEnumerable<MovimientoLedgerDto>>> ObtenerMovimientos(int usuarioId)
            {
                var billetera = await _context.Billeteras
                    .FirstOrDefaultAsync(b => b.UsuarioId == usuarioId);

                if (billetera == null)
                    return NotFound(new { mensaje = $"No se encontró la billetera para el usuario {usuarioId}." });

                var movimientos = await _context.TransaccionesLedger
                    .Where(t => t.BilleteraId == billetera.BilleteraId)
                    .OrderByDescending(t => t.Fecha)
                    .Select(t => new MovimientoLedgerDto
                    {
                        Id = t.TransaccionId,
                        Tipo = t.TipoTransaccion,
                        Monto = t.Monto,
                        Fecha = t.Fecha,
                        SubastaId = t.SubastaId
                    })
                    .ToListAsync();

                return Ok(movimientos);
            }
        [HttpPost("retirar/{usuarioId}")]
        public async Task<ActionResult<BilleteraResponseDto>> RetirarSaldo(int usuarioId, [FromBody] CargarSaldoDto dto)
        {
        if (dto.Monto <= 0)
            return BadRequest(new { mensaje = "El monto a retirar debe ser mayor a cero." });

        using var transaccion = await _context.Database.BeginTransactionAsync();
        try
        {
            var billetera = await _context.Billeteras
                .FirstOrDefaultAsync(b => b.UsuarioId == usuarioId);

            if (billetera == null)
                return NotFound(new { mensaje = $"No se encontró la billetera para el usuario {usuarioId}." });

            if (billetera.SaldoDisponible < dto.Monto)
                return BadRequest(new { mensaje = "Saldo disponible insuficiente para retirar." });

            // Descontar del SaldoTotal
            billetera.SaldoTotal -= dto.Monto;
            billetera.Version++;

            // Registrar en el Ledger como RETIRO
            _context.TransaccionesLedger.Add(new TransaccionLedger
            {
                BilleteraId = billetera.BilleteraId,
                TipoTransaccion = "RETIRO",
                Monto = -dto.Monto,
                Fecha = DateTime.UtcNow,
                SubastaId = null
            });

            // Registrar Auditoría
            _context.Auditorias.Add(new Auditoria_Log
            {
                Entidad = "BILLETERA",
                EntidadId = billetera.BilleteraId,
                Accion = "RETIRO_MANUAL_SALDO",
                UsuarioId = usuarioId,
                Detalle_Json = $"{{\"monto\": {dto.Monto}, \"nuevoSaldoTotal\": {billetera.SaldoTotal}}}",
                Fecha = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
            await transaccion.CommitAsync();

            var response = new BilleteraResponseDto
            {
                BilleteraId = billetera.BilleteraId,
                UsuarioId = billetera.UsuarioId,
                SaldoTotal = billetera.SaldoTotal,
                SaldoRetenido = billetera.SaldoRetenido,
                SaldoDisponible = billetera.SaldoDisponible,
                Version = billetera.Version
            };

            return Ok(new
            {
                mensaje = "Retiro realizado con éxito.",
                billetera = response
            });
        }
        catch (DbUpdateConcurrencyException)
        {
            await transaccion.RollbackAsync();
            return Conflict(new { mensaje = "Conflicto de concurrencia al actualizar la billetera." });
        }
        catch (Exception ex)
        {
            await transaccion.RollbackAsync();
            return StatusCode(500, new { mensaje = "Error interno al procesar el retiro.", detalle = ex.Message });
        }
        }
        }
    }