using System.ComponentModel.DataAnnotations;
namespace SubastaYa.API.DTOs
{
    // DTO para la solicitud de inicio de sesión
    public class LoginDto
    {
        [Required, EmailAddress ]
        public string Email { get; set; } = string.Empty;
        [Required]
         public string Password { get; set; } = string.Empty;
    }
}
