
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SubastaYa.API.Models
{
    public class Categoria
    {
        // Identificador de la tabla Categoria
        [Key]
        public int Id { get; set; }

        // Propiedades de la tabla Categoria
        [Required]
        public string Nombre { get; set; } = string.Empty;
        [Required]
        public string UrlIcono { get; set; } = string.Empty;

    }
}
