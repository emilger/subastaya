using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SubastaYa.API.Models
{
    public class Auditoria_Log
    {
        // Identificador de la tabla Auditoria_Log
        [Key]
        public int AuditoriaId { get; set; }
        public int UsuarioId { get; set; }

        // Propiedades de la tabla Auditoria_Log
        [Required]
        public string Entidad { get; set; } = string.Empty;
        public int EntidadId { get; set; }
        [Required]
        public string Accion { get; set; } = string.Empty;
        [Required] 
        public string Detalle_Json { get; set; } = string.Empty;
        [Required]
        public DateTime Fecha { get; set; }


    }
}
