using System.ComponentModel.DataAnnotations;

namespace SubastaYa.API.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [Required] 
        public string Alias { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        [Required]
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        // Relación 1 a 1 con Billetera
        public Billetera? Billetera { get; set; }

    }
}
