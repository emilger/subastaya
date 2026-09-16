namespace SubastaYa.API.DTOs
{
    public class BilleteraResponseDto
    {
        public int BilleteraId { get; set; }
        public int UsuarioId { get; set; }
        public decimal SaldoTotal { get; set; }
        public decimal SaldoRetenido { get; set; }
        public decimal SaldoDisponible { get; set; }
        public int Version { get; set; }
    }
}
