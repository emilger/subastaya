using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
namespace SubastaYa.API.Models
{
    public class Subasta
    {
        //identificadores de la tabla subasta
        [Key]
        public int SubastaId { get; set; }
        [Required]
        public int VendedorFK { get; set; }
        [Required]
        public int ProductoFK { get; set; }

        // propiedades de la tabla subasta
        [Required]
        public string Titulo { get; set; } = string.Empty;
        [Required]
        public string Descripcion { get; set; } = string.Empty;
        [Required]
        public string UrlImagen { get; set; } = string.Empty;
        [Required]
        public string Estado { get; set; } = string.Empty;
        [Required,ConcurrencyCheck]
        public int Version { get; set; }

        // propiedades de la tabla subasta relacionadas con la puja
        [Required]
        public decimal PrecioInicial { get; set; }
        [Required]
        public decimal PujaMinima { get; set; }

        // propiedades de la tabla subasta relacionadas con las fechas
        [Required]
        public DateTime FechaInicio { get; set; }
        [Required]
        public DateTime FechaFin { get; set; }

        // Relación orientada al Vendedor
        [ForeignKey("VendedorId")]
        public Usuario? Usuario { get; set; }

        // Relación orientada al Producto
        [ForeignKey("ProductoId")]
        public Categoria? Categoria { get; set; }

    }
}
