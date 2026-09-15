using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubastaYa.API.Data;
using SubastaYa.API.DTOs;
using SubastaYa.API.Models;

namespace SubastaYa.API.Controllers
{
    [ApiController]
    [Route("api/v1/wallets")]
    public class BilleteraController : ControllerBase
    {
        private readonly AplicationDbContext _context;

        public BilleteraController(AplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("users/{usuarioId}/balance")]
        public async Task<ActionResult<BilleteraResponseDto>> ObtenerBalance(int usuarioId)
        {
            var billetera = await _context.Billeteras
                .FirstOrDefaultAsync(b => b.UsuarioId == usuarioId);

            if (billetera == null)
                return NotFound(new { mensaje = $"No se encontró la billetera para el usuario {usuarioId}." });

            return Ok(new BilleteraResponseDto
            {
                Id = billetera.BilleteraId,
                UsuarioId = billetera.UsuarioId,
                SaldoTotal = billetera.SaldoTotal,
                SaldoRetenido = billetera.SaldoRetenido,
                SaldoDisponible = billetera.SaldoDisponible
            });
        }

        [HttpPost("users/{usuarioId}/deposits")]
        public async Task<IActionResult> DepositarFondos(int usuarioId, [FromBody] DepositoDto dto)
        {
            if (dto.Monto <= 0)
                return BadRequest(new { mensaje = "El monto a depositar debe ser mayor a cero." });

            using var transaction = await _context.Database.BeginTransactionAsync();
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

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new
                {
                    mensaje = "Depósito realizado con éxito.",
                    saldoTotal = billetera.SaldoTotal,
                    saldoDisponible = billetera.SaldoDisponible
                });
            }
            catch (DbUpdateConcurrencyException)
            {
                await transaction.RollbackAsync();
                return Conflict(new { mensaje = "Conflicto de concurrencia al actualizar la billetera." });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
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
    }
}