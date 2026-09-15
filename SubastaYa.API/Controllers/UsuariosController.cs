
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubastaYa.API.Data;
using SubastaYa.API.Models;

namespace SubastaYa.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly AplicationDbContext _context;

    public UsuariosController(AplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/usuarios (Obtener todos los usuarios)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
    {
        return await _context.Usuarios.ToListAsync();
    }

    // POST: api/usuarios (Crear un usuario nuevo)
    [HttpPost]
    public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUsuarios), new { id = usuario.UsuarioId }, usuario);
    }
}
