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
    [Route("api/[controller]")]
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var billetera = await _context.Billeteras
                .FirstOrDefaultAsync(b => b.UsuarioId == usuarioId);

            if (billetera == null)
            {
                return NotFound(new { mensaje = $"No se encontró la billetera para el usuario con ID {usuarioId}." });
            }

            // 1. Incrementar el Saldo Total
            billetera.SaldoTotal += dto.Monto;

            // 2. Registrar el movimiento en el Ledger (Auditoría Financiera)
            var transaccion = new TransaccionLedger
            {
                BilleteraId = billetera.BilleteraId,
                TipoTransaccion = "DEPOSITO",
                Monto = dto.Monto,
                Fecha = DateTime.UtcNow
            };

            _context.TransaccionesLedger.Add(transaccion);

            // 3. Persistir en la base de datos
            await _context.SaveChangesAsync();

            // 4. Mapear y responder
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
    }
}
