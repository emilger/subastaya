using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubastaYa.API.Data;
using SubastaYa.API.DTOs;
using SubastaYa.API.Models;

namespace SubastaYa.API.Controllers
{
    [ApiController]
    [Route("api/v1/auctions")]
    public class SubastaController : ControllerBase
    {
        private readonly AplicationDbContext _context;

        public SubastaController(AplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Procesa una oferta (puja) en tiempo real con reglas de garantía (Escrow), Anti-sniping y Concurrencia.
        /// </summary>
        [HttpPost("{id}/bids")]
        public async Task<IActionResult> RealizarPuja(int id, [FromBody] CrearPujaDto dto)
        {
            // 1. Iniciar transacción atómica
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var ahora = DateTime.UtcNow;

                // Cargar la subasta junto con sus pujas existentes
                var subasta = await _context.Subastas
                    .Include(s => s.Pujas)
                    .FirstOrDefaultAsync(s => s.SubastaId == id);

                if (subasta == null)
                    return NotFound(new { mensaje = "La subasta no existe." });

                // Validar estado y ventana de tiempo
                if (subasta.Estado != "ACTIVA" || subasta.FechaFin <= ahora)
                    return BadRequest(new { mensaje = "La subasta no está activa o ya ha finalizado." });

                // Validar que el comprador no sea el propio vendedor
                if (subasta.VendedorId == dto.CompradorId)
                    return BadRequest(new { mensaje = "El vendedor no puede ofertar en su propia subasta." });

                // 2. Determinar la puja más alta actual
                var pujaAnterior = subasta.Pujas
                    .OrderByDescending(p => p.MontoPuja)
                    .FirstOrDefault();

                decimal montoMinimoRequerido = pujaAnterior != null
                    ? pujaAnterior.MontoPuja + subasta.PujaMinima
                    : subasta.PrecioInicial;

                if (dto.Monto < montoMinimoRequerido)
                {
                    return BadRequest(new
                    {
                        mensaje = $"El monto debe ser de al menos ${montoMinimoRequerido} (Puja actual + incremento mínimo)."
                    });
                }

                // 3. Validar y actualizar Billetera del Nuevo Comprador
                var billeteraNuevoComprador = await _context.Billeteras
                    .FirstOrDefaultAsync(b => b.UsuarioId == dto.CompradorId);

                if (billeteraNuevoComprador == null)
                    return NotFound(new { mensaje = "Billetera del comprador no encontrada." });

                if (billeteraNuevoComprador.SaldoDisponible < dto.Monto)
                    return BadRequest(new { mensaje = "Saldo disponible insuficiente para respaldar esta puja en garantía (Escrow)." });

                // Congelar el nuevo saldo
                billeteraNuevoComprador.SaldoRetenido += dto.Monto;
                billeteraNuevoComprador.Version++;

                _context.TransaccionesLedger.Add(new TransaccionLedger
                {
                    BilleteraId = billeteraNuevoComprador.BilleteraId,
                    SubastaId = subasta.SubastaId,
                    TipoTransaccion = "RETENCION_PUJA",
                    Monto = -dto.Monto,
                    Fecha = ahora
                });

                // 4. Liberar garantía del Postor Anterior (si existe)
                if (pujaAnterior != null)
                {
                    var billeteraAnterior = await _context.Billeteras
                        .FirstOrDefaultAsync(b => b.UsuarioId == pujaAnterior.CompradorId);

                    if (billeteraAnterior != null)
                    {
                        billeteraAnterior.SaldoRetenido -= pujaAnterior.MontoPuja;
                        billeteraAnterior.Version++;

                        _context.TransaccionesLedger.Add(new TransaccionLedger
                        {
                            BilleteraId = billeteraAnterior.BilleteraId,
                            SubastaId = subasta.SubastaId,
                            TipoTransaccion = "LIBERACION_SUPERADO",
                            Monto = pujaAnterior.MontoPuja,
                            Fecha = ahora
                        });
                    }
                }

                // 5. Registrar la Nueva Puja
                var nuevaPuja = new Puja
                {
                    SubastaId = subasta.SubastaId,
                    CompradorId = dto.CompradorId,
                    MontoPuja = dto.Monto,
                    FechaPuja = ahora
                };
                _context.Pujas.Add(nuevaPuja);

                // 6. Aplicar Regla Anti-Sniping (Extensión de tiempo)
                bool tiempoExtendido = false;
                var tiempoRestante = subasta.FechaFin - ahora;

                if (tiempoRestante.TotalSeconds <= 60)
                {
                    subasta.FechaFin = subasta.FechaFin.AddMinutes(2);
                    tiempoExtendido = true;

                    _context.Auditorias.Add(new Auditoria_Log
                    {
                        Entidad = "SUBASTA",
                        EntidadId = subasta.SubastaId,
                        Accion = "EXTENSION_ANTI_SNIPING",
                        UsuarioId = dto.CompradorId,
                        Detalle_Json = $"{{\"mensaje\": \"Fecha de fin extendida 2 minutos por oferta de último segundo.\", \"nuevaFechaFin\": \"{subasta.FechaFin}\"}}",
                        Fecha = ahora
                    });
                }

                // Incrementar versión de concurrencia en la subasta
                subasta.Version++;

                // 7. Guardar cambios y confirmar transacción
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new
                {
                    mensaje = "Puja registrada exitosamente.",
                    pujaId = nuevaPuja.PujaId,
                    monto = nuevaPuja.MontoPuja,
                    tiempoExtendido = tiempoExtendido,
                    nuevaFechaFin = subasta.FechaFin
                });
            }
            catch (DbUpdateConcurrencyException)
            {
                await transaction.RollbackAsync();
                return Conflict(new { mensaje = "Hubo un conflicto de concurrencia. Otro usuario realizó una oferta al mismo tiempo. Reintente." });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new { mensaje = "Error interno al procesar la puja.", detalle = ex.Message });
            }
        }
    }
}