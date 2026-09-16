using System.ComponentModel.DataAnnotations;
namespace SubastaYa.API.DTOs
{
    public class CargarSaldoDto
    {
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto a cargar debe ser un valor positivo.")]
        public decimal Monto { get; set; }
    }
}
