namespace SubastaYa.API.DTOs
{
    public class BilleteraResponseDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public decimal SaldoTotal { get; set; }
        public decimal SaldoRetenido { get; set; }
        public decimal SaldoDisponible { get; set; }
    }

    public class DepositoDto
    {
        public decimal Monto { get; set; }
    }

    public class MovimientoLedgerDto
    {
        public int Id { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public int? SubastaId { get; set; }
    }
}
