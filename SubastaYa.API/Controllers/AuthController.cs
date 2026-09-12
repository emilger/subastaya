using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens; 
using System.IdentityModel.Tokens.Jwt; 
using System.Security.Claims; 
using System.Text; 
using SubastaYa.API.Data;
using SubastaYa.API.DTOs;
namespace SubastaYa.API.Controllers
{   // Controlador para manejar la autenticación de usuarios
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        // Constructor del controlador que recibe el contexto de la base de datos y la configuración
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration ;

        // Constructor del controlador que recibe el contexto de la base de datos y la configuración
        public AuthController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // Método para generar un token JWT para un usuario autenticado
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] DTOs.LoginDto loginDto)
        {
            // Buscar el usuario por correo electrónico
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            if (usuario == null)
            {
                return Unauthorized(new { message = "Usuario no encontrado" });
            }

            // Verificar la contraseña usando BCrypt
            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, usuario.ContrasenaHash))
            {
                return Unauthorized(new { message = "Contraseña incorrecta" });
            }

            // Generar un token JWT para el usuario autenticado
            var token = GenerarJwtToken(usuario.UsuarioId, usuario.Email, usuario.NombreUsuario);

            // Devolver la respuesta con el token y la información del usuario
            return Ok(new DTOs.AuthResponseDto
            {
                UsuarioId = usuario.UsuarioId,
                Token = token,
                Name = usuario.NombreUsuario,
                Email = usuario.Email
            });
        }

        // Método privado para generar un token JWT
        private string GenerarJwtToken(int usuarioId, string email, string nombre)
        { var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = Encoding.UTF8.GetBytes(jwtSettings["Secret"]!); 
            var claims = new[] { new Claim(JwtRegisteredClaimNames.Sub, usuarioId.ToString()), 
                new Claim(JwtRegisteredClaimNames.Email, email), new Claim("nombre", nombre), 
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) };
            var key = new SymmetricSecurityKey(secretKey); 
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); 
            var token = new JwtSecurityToken(issuer: jwtSettings["Issuer"], audience: jwtSettings["Audience"],
                claims: claims, expires: DateTime.UtcNow.AddHours(double.Parse(jwtSettings["ExpirationInHours"]!)), 
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token); 
        }

    }
}
