using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SubastaYa.API.Models;

public class Puja
{
    // identificador único de la puja
    [Key]
    public int PujaId { get; set; }
    public int SubastaId { get; set; }
    public int CompradorId { get; set; }

    // datos relacionados con la fecha y hora de la puja, y el monto de la puja
    [Required]
    public DateTime FechaPuja { get; set; } = DateTime.UtcNow;

    [Required]
    public decimal MontoPuja { get; set; }

    // Propiedades de navegación individuales
    [ForeignKey("SubastaId")]
    public Subasta Subasta { get; set; } = null!;

    [ForeignKey("CompradorId")]
    public Usuario Comprador { get; set; } = null!;
}