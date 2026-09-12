namespace SubastaYa.API.DTOs
{
    // DTO para la respuesta de autenticación
    public class AuthResponseDto
    { public int UsuarioId { get; set; }
        public string Token { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty; 
        public string Email { get; set; } = string.Empty;
    }
}
