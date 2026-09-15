namespace SubastaYa.API.DTOs
{
    public class SubastaResponseDto
    { 
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string UrlImagen { get; set; } = string.Empty;
        public decimal PrecioBase { get; set; }
        public decimal IncrementoMinimo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Estado { get; set; } = string.Empty;

        public int CategoriaId { get; set; }
        public string NombreCategoria { get; set; } = string.Empty;

        public int VendedorId { get; set; }
        public string NombreVendedor { get; set; } = string.Empty;

        public decimal OfertaMasAlta { get; set; }
        public int CantidadOfertas { get; set; }
    }
}
