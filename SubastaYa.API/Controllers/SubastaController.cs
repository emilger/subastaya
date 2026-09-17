using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubastaYa.API.Data;
using SubastaYa.API.DTOs;
using SubastaYa.API.Models;
using SubastaYa.API.Services;
using System.Security.Claims;

namespace SubastaYa.API.Controllers
{
    // Controller para manejar operaciones relacionadas con las subastas.
    [ApiController]
    [Authorize]
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

        /// Crea una nueva subasta con validaciones básicas de fechas y datos.
       [HttpPost]
        public async Task<IActionResult> CrearSubasta([FromBody] CrearSubastaDto dto)
        {
            try
            {
                var ahora = DateTime.UtcNow;

                //Extraer dinámicamente el ID del Vendedor desde el Token JWT
                var vendedorIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
                                   ?? User.FindFirst("sub")?.Value;

                if (string.IsNullOrEmpty(vendedorIdClaim) || !int.TryParse(vendedorIdClaim, out int vendedorId))
                {
                    return Unauthorized(new { mensaje = "No se pudo identificar al usuario desde el token JWT." });
                }

                //Validaciones de negocio
                if (dto.FechaFin <= dto.FechaInicio)
                    return BadRequest(new { mensaje = "La fecha de fin debe ser posterior a la fecha de inicio." });

                //Verificar que la categoría exista en la base de datos
                var categoriaExiste = await _context.Categorias.AnyAsync(c => c.CategoriaId == dto.CategoriaId);
                if (!categoriaExiste)
                    return BadRequest(new { mensaje = $"La categoría con ID {dto.CategoriaId} no existe en la base de datos." });

                //Instanciar la Subasta
                var nuevaSubasta = new Subasta
                {
                    Titulo = dto.Titulo,
                    Descripcion = dto.Descripcion,
                    UrlImagen = dto.UrlImagen,
                    CategoriaId = dto.CategoriaId,
                    PrecioInicial = dto.PrecioBase,
                    PujaMinima = dto.IncrementoMinimo,
                    FechaInicio = DateTime.SpecifyKind(dto.FechaInicio.AddHours(3), DateTimeKind.Utc),
                    FechaFin = DateTime.SpecifyKind(dto.FechaFin.AddHours(3), DateTimeKind.Utc),
                    Estado = "ACTIVA",
                    Version = 1,
                    VendedorId = vendedorId
                };

                _context.Subastas.Add(nuevaSubasta);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(CrearSubasta), new { id = nuevaSubasta.SubastaId }, nuevaSubasta);
            }
            catch (Exception ex)
            {
                // Devuelve el mensaje exacto de error interno para diagnosticar en Postman
                return StatusCode(500, new 
                { 
                    mensaje = "Error interno al guardar la subasta en la base de datos.",
                    error = ex.Message, 
                    detalle = ex.InnerException?.Message 
                });
            }
        }

        // Procesa una oferta (puja) en tiempo real con reglas de garantía (Escrow), Anti-sniping y Concurrencia.
        [HttpPost("{id}/bids")]
        public async Task<IActionResult> RealizarPuja(int id, [FromBody] CrearPujaDto dto)
        {
            // Extraer dinámicamente el ID del comprador autenticado desde el Token JWT
            var compradorIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                                ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(compradorIdClaim) || !int.TryParse(compradorIdClaim, out int compradorId))
            {
                return Unauthorized(new { mensaje = "No se pudo identificar al usuario desde el token JWT." });
            }
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
                    if (subasta.VendedorId == compradorId)
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
                        .FirstOrDefaultAsync(b => b.UsuarioId == compradorId);

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
                        CompradorId = compradorId,
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
                            UsuarioId = compradorId,
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
        }

        /// Configura o actualiza la puja automática (Proxy Bidding) para un usuario en una subasta.
        [HttpPost("{id}/auto-bids")]
        public async Task<IActionResult> ConfigurarPujaAutomatica(int id, [FromBody] CrearPujaAutomaticaDto dto)
        {
            // Extraer dinámicamente el ID del comprador autenticado desde el Token JWT
            var compradorIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                                ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(compradorIdClaim) || !int.TryParse(compradorIdClaim, out int compradorId))
            {
                return Unauthorized(new { mensaje = "No se pudo identificar al usuario desde el token JWT." });
            }

           var ahora = DateTime.UtcNow;

            var subasta = await _context.Subastas.FindAsync(id);
            if (subasta == null)
                return NotFound(new { mensaje = "La subasta no existe." });

            if (subasta.Estado != "ACTIVA" || subasta.FechaFin <= ahora)
                return BadRequest(new { mensaje = "La subasta no está activa o ya ha finalizado." });

            if (subasta.VendedorId == compradorId)
                return BadRequest(new { mensaje = "El vendedor no puede configurar pujas automáticas en su propia subasta." });

            // Buscar si ya existe una configuración para esta subasta y comprador
            var autoPujaExistente = await _context.PujasAutomaticas
                .FirstOrDefaultAsync(pa => pa.SubastaId == id && pa.CompradorId == compradorId);

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
                    CompradorId = compradorId,
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
    /// Consulta el catálogo general de subastas con filtros opcionales (Público).
    /// </summary>
    [HttpGet]
    [AllowAnonymous] // 👈 Permite el acceso público sin Token JWT
    public async Task<IActionResult> ObtenerSubastas(
        [FromQuery] string? estado,
        [FromQuery] int? categoriaId,
        [FromQuery] string? busqueda)
    {
        try
        {
            var query = _context.Subastas
                .Include(s => s.categoria)
                .Include(s => s.Pujas)
                .AsQueryable();

            // 1. Filtro opcional por Estado (ej: ACTIVA, PROGRAMADA, FINALIZADA)
            if (!string.IsNullOrEmpty(estado))
            {
                query = query.Where(s => s.Estado.ToUpper() == estado.ToUpper());
            }

            // 2. Filtro opcional por Categoría
            if (categoriaId.HasValue)
            {
                query = query.Where(s => s.CategoriaId == categoriaId.Value);
            }

            // 3. Búsqueda por palabra clave en Título o Descripción
            if (!string.IsNullOrEmpty(busqueda))
            {
                var termino = busqueda.ToLower();
                query = query.Where(s => s.Titulo.ToLower().Contains(termino) ||
                                         s.Descripcion.ToLower().Contains(termino));
            }

            var subastas = await query
                .OrderByDescending(s => s.FechaInicio)
                .Select(s => new
                {
                    s.SubastaId,
                    s.Titulo,
                    s.Descripcion,
                    s.UrlImagen,
                    s.CategoriaId,
                    NombreCategoria = s.categoria != null ? s.categoria.Nombre : null,
                    s.PrecioInicial,
                    s.PujaMinima,
                    s.FechaInicio,
                    s.FechaFin,
                    s.Estado,
                    s.VendedorId,
                    CantidadPujas = s.Pujas.Count,
                    OfertaMasAlta = s.Pujas.Any() ? s.Pujas.Max(p => p.MontoPuja) : s.PrecioInicial
                })
                .ToListAsync();

            return Ok(subastas);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { mensaje = "Error interno al consultar el catálogo de subastas.", detalle = ex.Message });
        }
    }

    /// Obtiene el detalle completo de una subasta y su historial de ofertas (Público).
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> ObtenerSubastaPorId(int id)
    {
        try
        {
            var subasta = await _context.Subastas
                .Include(s => s.categoria)
                .Include(s => s.Vendedor)
                .Include(s => s.Pujas)
                    .ThenInclude(p => p.Comprador)
                .FirstOrDefaultAsync(s => s.SubastaId == id);

            if (subasta == null)
                return NotFound(new { mensaje = "La subasta solicitada no existe." });

            var pujaMasAlta = subasta.Pujas.OrderByDescending(p => p.MontoPuja).FirstOrDefault();

            var respuesta = new
            {
                subasta.SubastaId,
                subasta.Titulo,
                subasta.Descripcion,
                subasta.UrlImagen,
                Categoria = subasta.categoria != null ? new
                {
                    subasta.categoria.CategoriaId,
                    subasta.categoria.Nombre
                } : null,
                Vendedor = subasta.Vendedor != null ? new
                {
                    subasta.Vendedor.UsuarioId,
                    subasta.Vendedor.NombreUsuario,
                    subasta.Vendedor.Email
                } : null,
                subasta.PrecioInicial,
                subasta.PujaMinima,
                subasta.FechaInicio,
                subasta.FechaFin,
                subasta.Estado,
                subasta.Version,
                OfertaLiderActual = pujaMasAlta != null ? pujaMasAlta.MontoPuja : subasta.PrecioInicial,
                ProximaPujaSugerida = pujaMasAlta != null ? pujaMasAlta.MontoPuja + subasta.PujaMinima : subasta.PrecioInicial,
                HistorialPujas = subasta.Pujas
                    .OrderByDescending(p => p.FechaPuja)
                    .Select(p => new
                    {
                        p.PujaId,
                        p.MontoPuja,
                        p.FechaPuja,
                        CompradorAlias = p.Comprador != null ? p.Comprador.Alias : $"Comprador #{p.CompradorId}"
                    })
                    .ToList()
            };

            return Ok(respuesta);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { mensaje = "Error interno al consultar la subasta.", detalle = ex.Message });
        }
    }
    }
}