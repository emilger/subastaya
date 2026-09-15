using System.ComponentModel.DataAnnotations;

namespace SubastaYa.API.DTOs
{
    public class CrearSubastaDto
    {
        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(100, ErrorMessage = "El título no puede superar los 100 caracteres.")]
        [RegularExpression(@"^[a-zA-Z0-9 áéíóúÁÉÍÓÚñÑ\\-\_.,()]+$", ErrorMessage = "El título contiene caracteres no válidos.")] 
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        public string Descripcion { get; set; } = string.Empty;

        [Required(ErrorMessage = "La URL de la imagen es obligatoria.")]
        public string UrlImagen { get; set; } = string.Empty;

        [Required(ErrorMessage = "La categoría es obligatoria.")]
        public int CategoriaId { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El precio base debe ser mayor a 0.")]
        public decimal PrecioBase { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El incremento mínimo debe ser mayor a 0.")]
        public decimal IncrementoMinimo { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "La fecha de finalización es obligatoria.")]
        public DateTime FechaFin { get; set; }
    }
}
