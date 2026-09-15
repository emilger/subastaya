using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubastaYa.API.Data;
using SubastaYa.API.DTOs;
using SubastaYa.API.Models;
namespace SubastaYa.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubastaController : ControllerBase
    {
        private readonly AplicationDbContext _context;

        public SubastaController(AplicationDbContext context)
        {
            _context = context;
        }

        //implementacion de filtro de subastas por categoria, estado o termino de busqueda
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubastaResponseDto>>> GetSubastas(
            [FromQuery] string? estado,
            [FromQuery] int? categoria,
            [FromQuery] string? Busqueda
            )
        {
            var query = _context.Subastas
                .Include(s => s.categoria)
                .Include(s => s.Vendedor)
                .Include(s => s.Pujas)
                .AsQueryable();

            if (!string.IsNullOrEmpty(estado))
            {
                query = query.Where(s => s.Estado == estado);
            }

            if (categoria.HasValue)
            {

                query = query.Where(s => s.CategoriaId == categoria.Value);
            }
            if (!string.IsNullOrEmpty(Busqueda))
            {
                query = query.Where(s => s.Titulo.ToLower().Contains(Busqueda.ToLower()) ||
                                         s.Descripcion.ToLower().Contains(Busqueda.ToLower()));
            }

            var response = await query.Select(s => new SubastaResponseDto
            {
                Id = s.SubastaId,
                Titulo = s.Titulo,
                Descripcion = s.Descripcion,
                UrlImagen = s.UrlImagen,
                PrecioBase = s.PrecioInicial,
                IncrementoMinimo = s.PujaMinima,
                FechaInicio = s.FechaInicio,
                FechaFin = s.FechaFin,
                Estado = s.Estado,
                CategoriaId = s.CategoriaId,
                NombreCategoria = s.categoria != null ? s.categoria.Nombre : string.Empty,
                VendedorId = s.VendedorId,
                NombreVendedor = s.Vendedor != null ? s.Vendedor.NombreUsuario : string.Empty,
                OfertaMasAlta = s.Pujas.Any() ? s.Pujas.Max(p => p.MontoPuja) : s.PrecioInicial,
                CantidadOfertas = s.Pujas.Count
            }).ToListAsync();
            return Ok(response);
        }

        // GET: api/subastas/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<SubastaResponseDto>> GetSubasta(int id)
        {
            var subasta = await _context.Subastas
                .Include(s => s.categoria)
                .Include(s => s.Vendedor)
                .Include(s => s.Pujas)
                .FirstOrDefaultAsync(s => s.SubastaId == id);

            if (subasta == null)
            {
                return NotFound(new { mensaje = "Subasta no encontrada." });
            }

            var response = new SubastaResponseDto
            {
                Id = subasta.SubastaId,
                Titulo = subasta.Titulo,
                Descripcion = subasta.Descripcion,
                UrlImagen = subasta.UrlImagen,
                PrecioBase = subasta.PrecioInicial,
                IncrementoMinimo = subasta.PujaMinima,
                FechaInicio = subasta.FechaInicio,
                FechaFin = subasta.FechaFin,
                Estado = subasta.Estado,
                CategoriaId = subasta.CategoriaId,
                NombreCategoria = subasta.categoria != null ? subasta.categoria.Nombre : string.Empty,
                VendedorId = subasta.VendedorId,
                NombreVendedor = subasta.Vendedor != null ? subasta.Vendedor.NombreUsuario : string.Empty,
                OfertaMasAlta = subasta.Pujas.Any() ? subasta.Pujas.Max(p => p.MontoPuja) : subasta.PrecioInicial,
                CantidadOfertas = subasta.Pujas.Count
            };

            return Ok(response);
        }

        // POST: api/subastas
        // Requiere token JWT. Extrae el VendedorId del usuario logueado.
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<SubastaResponseDto>> CrearSubasta([FromBody] CrearSubastaDto dto)
        {
            if (dto.FechaFin <= dto.FechaInicio)
            {
                return BadRequest(new { mensaje = "La fecha de finalización debe ser posterior a la fecha de inicio." });
            }

            var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                                 ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(usuarioIdClaim) || !int.TryParse(usuarioIdClaim, out int vendedorId))
            {
                return Unauthorized(new { mensaje = "Token no válido o sin identificación de usuario." });
            }

            var categoriaExiste = await _context.Categorias.AnyAsync(c => c.CategoriaId == dto.CategoriaId);
            if (!categoriaExiste)
            {
                return BadRequest(new { mensaje = "La categoría especificada no existe." });
            }

            string estadoInicial = dto.FechaInicio <= DateTime.UtcNow ? "ACTIVA" : "PROGRAMADA";

            var nuevaSubasta = new Subasta
            {
                VendedorId = vendedorId,
                CategoriaId = dto.CategoriaId,
                Titulo = dto.Titulo,
                Descripcion = dto.Descripcion,
                UrlImagen = dto.UrlImagen,
                PrecioInicial = dto.PrecioBase,
                PujaMinima = dto.IncrementoMinimo,
                FechaInicio = dto.FechaInicio.ToUniversalTime(),
                FechaFin = dto.FechaFin.ToUniversalTime(),
                Estado = estadoInicial,
                Version = 1
            };

            _context.Subastas.Add(nuevaSubasta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSubasta), new { id = nuevaSubasta.SubastaId }, new SubastaResponseDto
            {
                Id = nuevaSubasta.SubastaId,
                Titulo = nuevaSubasta.Titulo,
                Descripcion = nuevaSubasta.Descripcion,
                UrlImagen = nuevaSubasta.UrlImagen,
                PrecioBase = nuevaSubasta.PrecioInicial,
                IncrementoMinimo = nuevaSubasta.PujaMinima,
                FechaInicio = nuevaSubasta.FechaInicio,
                FechaFin = nuevaSubasta.FechaFin,
                Estado = nuevaSubasta.Estado,
                CategoriaId = nuevaSubasta.CategoriaId,
                VendedorId = nuevaSubasta.VendedorId,
                OfertaMasAlta = nuevaSubasta.PrecioInicial,
                CantidadOfertas = 0
            });
        }
    }
}