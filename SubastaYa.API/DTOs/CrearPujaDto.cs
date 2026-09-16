using System.ComponentModel.DataAnnotations;
namespace SubastaYa.API.DTOs
{
    public class CrearPujaDto
    {
        [Required]
        public int SubastaId { get; set; }
        [Required]
        public int CompradorId { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto a cargar debe ser un valor positivo.")]
        public decimal MontoPuja { get; set; }
    }
}
