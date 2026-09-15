using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace SubastaYa.API.Models
{
    public class Usuario
    {
        // Identificador único del usuario
        [Key]
        public int UsuarioId { get; set; }

        // Propiedades del usuario
        [Required]
        public string NombreUsuario { get; set; } = string.Empty;
        [Required] 
        public string Alias { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string ContrasenaHash { get; set; } = string.Empty;
        [Required]
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        // Relación 1 a 1 con Billetera
        public Billetera? Billetera { get; set; }
        // relacion 1 a muchos con Auditoria_Log
        public ICollection<Auditoria_Log> Auditorias { get; set; } = new List<Auditoria_Log>();
        // Relación 1 a muchos con Subastas 
        public ICollection<Subasta> Subastas { get; set; } = new List<Subasta>();


    }
}
