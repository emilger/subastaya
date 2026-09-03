using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SubastaYa.API.Models;

public class Puja
{
    [Key]
    public int PujaId { get; set; }

    [Required]
    public int SubastaId { get; set; }

    [Required]
    public int CompradorId { get; set; }

    [Required]
    public DateTime FechaPuja { get; set; } = DateTime.UtcNow;

    [Required]
    public decimal MontoPuja { get; set; }

    // Propiedades de navegación individuales
    [ForeignKey("SubastaId")]
    public Subasta? Subasta { get; set; }

    [ForeignKey("CompradorId")]
    public Usuario? Comprador { get; set; }
}