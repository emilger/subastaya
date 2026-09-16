using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubastaYa.API.Data;
using SubastaYa.API.DTOs;
using SubastaYa.API.Models;
using SubastaYa.API.Services;

namespace SubastaYa.API.Controllers
{
    // Controller para manejar operaciones relacionadas con las subastas.
    [ApiController]
    [Route("api/v1/auctions")]
    public class SubastaController : ControllerBase
    {
        private readonly AplicationDbContext _context;
        private readonly PujaAutomaticaService _autoPujaService;

        public SubastaController(AplicationDbContext context, PujaAutomaticaService autoPujaService)
        {
            _context = context;
            _autoPujaService = autoPujaService;
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

                if (dto.MontoPuja < montoMinimoRequerido)
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

                if (billeteraNuevoComprador.SaldoDisponible < dto.MontoPuja)
                    return BadRequest(new { mensaje = "Saldo disponible insuficiente para respaldar esta puja en garantía (Escrow)." });

                // Congelar el nuevo saldo
                billeteraNuevoComprador.SaldoRetenido += dto.MontoPuja;
                billeteraNuevoComprador.Version++;

                _context.TransaccionesLedger.Add(new TransaccionLedger
                {
                    BilleteraId = billeteraNuevoComprador.BilleteraId,
                    SubastaId = subasta.SubastaId,
                    TipoTransaccion = "RETENCION_PUJA",
                    Monto = -dto.MontoPuja,
                    Fecha = ahora
                });

                // 4. Liberar saldo del postor anterior (si existe)
                if (pujaAnterior != null)
                {
                    var billeteraAnteriorComprador = await _context.Billeteras
                        .FirstOrDefaultAsync(b => b.UsuarioId == pujaAnterior.CompradorId);

                    if (billeteraAnteriorComprador != null)
                    {
                        billeteraAnteriorComprador.SaldoRetenido -= pujaAnterior.MontoPuja;
                        billeteraAnteriorComprador.Version++;

                        _context.TransaccionesLedger.Add(new TransaccionLedger
                        {
                            BilleteraId = billeteraAnteriorComprador.BilleteraId,
                            SubastaId = subasta.SubastaId,
                            TipoTransaccion = "LIBERACION_SUPERADO",
                            Monto = pujaAnterior.MontoPuja,
                            Fecha = ahora
                        });
                    }
                }

                // 5. Registrar la nueva puja
                var nuevaPuja = new Puja
                {
                    SubastaId = subasta.SubastaId,
                    CompradorId = dto.CompradorId,
                    MontoPuja = dto.MontoPuja,
                    FechaPuja = ahora
                };
                _context.Pujas.Add(nuevaPuja);

                // 6. Regla Anti-Sniping
                if ((subasta.FechaFin - ahora).TotalSeconds <= 60)
                {
                    subasta.FechaFin = subasta.FechaFin.AddMinutes(2);
                    _context.Auditorias.Add(new Auditoria_Log
                    {
                        UsuarioId = dto.CompradorId,
                        Entidad = "Subasta",
                        EntidadId = subasta.SubastaId,
                        Accion = "EXTENSION_ANTI_SNIPING",
                        Detalle_Json = "Extensión de 2 minutos aplicada por puja en el último minuto.",
                        Fecha = ahora
                    });
                }

                subasta.Version++;

                // Persistir cambios y confirmar transacción
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                // 7. Evaluar motor de pujas automáticas tras completar la oferta manual
                await _autoPujaService.ProcesarPujasAutomaticasAsync(subasta.SubastaId, ahora);

                return Ok(new
                {
                    mensaje = "Puja realizada con éxito.",
                    montoPuja = dto.MontoPuja,
                    fechaFin = subasta.FechaFin
                });
            }
            catch (DbUpdateConcurrencyException)
            {
                await transaction.RollbackAsync();
                return Conflict(new { mensaje = "Conflicto de concurrencia: otra oferta o actualización de billetera ocurrió simultáneamente. Intente nuevamente." });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new { mensaje = "Ocurrió un error inesperado al procesar la puja.", detalle = ex.Message });
            }
        }

        /// <summary>
        /// Configura o actualiza la puja automática (Proxy Bidding) para un usuario en una subasta.
        /// </summary>
        [HttpPost("{id}/auto-bids")]
        public async Task<IActionResult> ConfigurarPujaAutomatica(int id, [FromBody] CrearPujaAutomaticaDto dto)
        {
            var ahora = DateTime.UtcNow;

            var subasta = await _context.Subastas.FindAsync(id);
            if (subasta == null)
                return NotFound(new { mensaje = "La subasta no existe." });

            if (subasta.Estado != "ACTIVA" || subasta.FechaFin <= ahora)
                return BadRequest(new { mensaje = "La subasta no está activa o ya ha finalizado." });

            if (subasta.VendedorId == dto.CompradorId)
                return BadRequest(new { mensaje = "El vendedor no puede configurar pujas automáticas en su propia subasta." });

            // Buscar si ya existe una configuración para esta subasta y comprador
            var autoPujaExistente = await _context.PujasAutomaticas
                .FirstOrDefaultAsync(pa => pa.SubastaId == id && pa.CompradorId == dto.CompradorId);

            if (autoPujaExistente != null)
            {
                autoPujaExistente.MontoMaximo = dto.MontoMaximo;
                autoPujaExistente.Activa = true;
                autoPujaExistente.FechaConfiguracion = ahora;
            }
            else
            {
                _context.PujasAutomaticas.Add(new PujaAutomatica
                {
                    SubastaId = id,
                    CompradorId = dto.CompradorId,
                    MontoMaximo = dto.MontoMaximo,
                    Activa = true,
                    FechaConfiguracion = ahora
                });
            }

            await _context.SaveChangesAsync();

            // Disparar inmediatamente el motor por si esta configuración activa una contraoferta
            await _autoPujaService.ProcesarPujasAutomaticasAsync(id, ahora);

            return Ok(new
            {
                mensaje = "Puja automática configurada correctamente.",
                montoMaximo = dto.MontoMaximo
            });
        }
    }
}