using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SubastaYa.API.Models
{
    public class PujaAutomatica
    {
        [Key]
        public int PujaAutomaticaId { get; set; }

        [Required]
        public int SubastaId { get; set; }

        [Required]
        public int CompradorId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal MontoMaximo { get; set; }

        public bool Activa { get; set; } = true;

        public DateTime FechaConfiguracion { get; set; } = DateTime.UtcNow;

        [ForeignKey("SubastaId")]
        public virtual Subasta Subasta { get; set; } = null!;

        [ForeignKey("CompradorId")]
        public virtual Usuario Comprador { get; set; } = null!;
    }
}
