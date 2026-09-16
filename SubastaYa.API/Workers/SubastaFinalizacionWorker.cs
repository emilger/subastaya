using Microsoft.EntityFrameworkCore;
using SubastaYa.API.Data;
using SubastaYa.API.Models;

namespace SubastaYa.API.Workers
{
    public class SubastaFinalizacionWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<SubastaFinalizacionWorker> _logger;

        public SubastaFinalizacionWorker(IServiceProvider serviceProvider, ILogger<SubastaFinalizacionWorker> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Servicio en segundo plano para cierre de subastas iniciado.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await ProcesarSubastasVencidasAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al procesar la finalización de subastas vencidas.");
                }

                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }

        private async Task ProcesarSubastasVencidasAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AplicationDbContext>();

            var ahora = DateTime.UtcNow;

            var subastasVencidas = await context.Subastas
                .Include(s => s.Pujas)
                .Where(s => s.Estado == "ACTIVA" && s.FechaFin <= ahora)
                .ToListAsync(stoppingToken);

            foreach (var subasta in subastasVencidas)
            {
                using var transaction = await context.Database.BeginTransactionAsync(stoppingToken);
                try
                {
                    var pujaGanadora = subasta.Pujas
                        .OrderByDescending(p => p.MontoPuja)
                        .FirstOrDefault();

                    if (pujaGanadora == null)
                    {
                        // CASO 1: Sin pujas -> DESIERTA
                        subasta.Estado = "DESIERTA";

                        context.Auditorias.Add(new Auditoria_Log
                        {
                            Entidad = "SUBASTA",
                            EntidadId = subasta.SubastaId,
                            Accion = "CERRAR_SUBASTA_DESIERTA",
                            UsuarioId = subasta.VendedorId,
                            Detalle_Json = $"{{\"mensaje\": \"Subasta {subasta.SubastaId} declarada desierta por falta de ofertas.\"}}",
                            Fecha = DateTime.UtcNow
                        });
                    }
                    else
                    {
                        // CASO 2: Con pujas -> FINALIZADA y LIQUIDACIÓN DE SALDOS
                        subasta.Estado = "FINALIZADA";

                        // Comprador: Descontar de SaldoTotal y SaldoRetenido
                        var billeteraComprador = await context.Billeteras
                            .FirstOrDefaultAsync(b => b.UsuarioId == pujaGanadora.CompradorId, stoppingToken);

                        if (billeteraComprador != null)
                        {
                            billeteraComprador.SaldoRetenido -= pujaGanadora.MontoPuja;
                            billeteraComprador.SaldoTotal -= pujaGanadora.MontoPuja;
                            billeteraComprador.Version++;

                            context.TransaccionesLedger.Add(new TransaccionLedger
                            {
                                BilleteraId = billeteraComprador.BilleteraId,
                                TipoTransaccion = "DEBITO_SUBASTA_GANADA",
                                Monto = -pujaGanadora.MontoPuja,
                                Fecha = DateTime.UtcNow,
                                SubastaId = subasta.SubastaId
                            });
                        }

                        // Vendedor: Acreditar en SaldoTotal
                        var billeteraVendedor = await context.Billeteras
                            .FirstOrDefaultAsync(b => b.UsuarioId == subasta.VendedorId, stoppingToken);

                        if (billeteraVendedor != null)
                        {
                            billeteraVendedor.SaldoTotal += pujaGanadora.MontoPuja;
                            billeteraVendedor.Version++;

                            context.TransaccionesLedger.Add(new TransaccionLedger
                            {
                                BilleteraId = billeteraVendedor.BilleteraId,
                                TipoTransaccion = "ACREDITACION_VENTA",
                                Monto = pujaGanadora.MontoPuja,
                                Fecha = DateTime.UtcNow,
                                SubastaId = subasta.SubastaId
                            });
                        }

                        // Auditoría
                        context.Auditorias.Add(new Auditoria_Log
                        {
                            Entidad = "SUBASTA",
                            EntidadId = subasta.SubastaId,
                            Accion = "CERRAR_SUBASTA_FINALIZADA",
                            UsuarioId = pujaGanadora.CompradorId,
                            Detalle_Json = $"{{\"compradorId\": {pujaGanadora.CompradorId}, \"montoFinal\": {pujaGanadora.MontoPuja}}}",
                            Fecha = DateTime.UtcNow
                        });
                    }

                    await context.SaveChangesAsync(stoppingToken);
                    await transaction.CommitAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(stoppingToken);
                    _logger.LogError(ex, "Error al cerrar la subasta {SubastaId}.", subasta.SubastaId);
                }
            }
        }
    }
}