using Microsoft.EntityFrameworkCore;
using SubastaYa.API.Data;
using SubastaYa.API.Models;

namespace SubastaYa.API.Services
{
    public class PujaAutomaticaService
    {
        private readonly AplicationDbContext _context;

        public PujaAutomaticaService(AplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Evalúa y ejecuta contraofertas automáticas en una subasta de manera recursiva/iterativa.
        /// </summary>
        public async Task ProcesarPujasAutomaticasAsync(int subastaId, DateTime ahora)
        {
            bool huboNuevaPuja = true;

            // Bucle para procesar contraofertas entre múltiples usuarios con puja automática
            while (huboNuevaPuja)
            {
                huboNuevaPuja = false;

                var subasta = await _context.Subastas
                    .Include(s => s.Pujas)
                    .FirstOrDefaultAsync(s => s.SubastaId == subastaId);

                if (subasta == null || subasta.Estado != "ACTIVA" || subasta.FechaFin <= ahora)
                    break;

                var pujaActual = subasta.Pujas
                    .OrderByDescending(p => p.MontoPuja)
                    .FirstOrDefault();

                int? actualLiderId = pujaActual?.CompradorId;
                decimal montoActual = pujaActual?.MontoPuja ?? subasta.PrecioInicial;
                decimal siguienteMontoRequerido = pujaActual != null
                    ? montoActual + subasta.PujaMinima
                    : subasta.PrecioInicial;

                // Buscar la puja automática activa de un comprador que NO sea el líder actual y cuyo máximo cubra el siguiente monto
                var autoPujaCandidata = await _context.PujasAutomaticas
                    .Where(pa => pa.SubastaId == subastaId
                              && pa.Activa
                              && pa.CompradorId != actualLiderId
                              && pa.MontoMaximo >= siguienteMontoRequerido)
                    .OrderByDescending(pa => pa.MontoMaximo)
                    .ThenBy(pa => pa.FechaConfiguracion) // En empate, prioridad al que la configuró primero
                    .FirstOrDefaultAsync();

                if (autoPujaCandidata == null)
                    break; // No hay contraofertas válidas disponibles

                // Validar Billetera del nuevo ofertante automático
                var billetera = await _context.Billeteras
                    .FirstOrDefaultAsync(b => b.UsuarioId == autoPujaCandidata.CompradorId);

                if (billetera == null || billetera.SaldoDisponible < siguienteMontoRequerido)
                {
                    // Desactivar la puja automática por saldo insuficiente
                    autoPujaCandidata.Activa = false;
                    await _context.SaveChangesAsync();
                    continue;
                }

                // 1. Congelar saldo del comprador automático
                billetera.SaldoRetenido += siguienteMontoRequerido;
                billetera.Version++;

                _context.TransaccionesLedger.Add(new TransaccionLedger
                {
                    BilleteraId = billetera.BilleteraId,
                    SubastaId = subasta.SubastaId,
                    TipoTransaccion = "RETENCION_PUJA_AUTO",
                    Monto = -siguienteMontoRequerido,
                    Fecha = ahora
                });

                // 2. Liberar saldo del postor anterior (si existe)
                if (pujaActual != null)
                {
                    var billeteraAnterior = await _context.Billeteras
                        .FirstOrDefaultAsync(b => b.UsuarioId == pujaActual.CompradorId);

                    if (billeteraAnterior != null)
                    {
                        billeteraAnterior.SaldoRetenido -= pujaActual.MontoPuja;
                        billeteraAnterior.Version++;

                        _context.TransaccionesLedger.Add(new TransaccionLedger
                        {
                            BilleteraId = billeteraAnterior.BilleteraId,
                            SubastaId = subasta.SubastaId,
                            TipoTransaccion = "LIBERACION_SUPERADO_AUTO",
                            Monto = pujaActual.MontoPuja,
                            Fecha = ahora
                        });
                    }
                }

                // 3. Registrar la nueva puja realizada por el bot/motor
                _context.Pujas.Add(new Puja
                {
                    SubastaId = subasta.SubastaId,
                    CompradorId = autoPujaCandidata.CompradorId,
                    MontoPuja = siguienteMontoRequerido,
                    FechaPuja = ahora
                });

                // 4. Regla Anti-Sniping
                if ((subasta.FechaFin - ahora).TotalSeconds <= 60)
                {
                    subasta.FechaFin = subasta.FechaFin.AddMinutes(2);
                }

                subasta.Version++;
                await _context.SaveChangesAsync();

                // Indicar que se registró una puja para evaluar si otro bot responde
                huboNuevaPuja = true;
            }
        }
    }
}
