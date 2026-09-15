using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SubastaYa.API.Models
{
    public class Billetera
    {
        [Key]
        public int BilleteraId { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        public decimal SaldoTotal { get; set; }
        public decimal SaldoRetenido { get; set; }

        // Mapeado a la columna física calculada en PostgreSQL
        public decimal SaldoDisponible { get; set; }

        [ConcurrencyCheck]
        public int Version { get; set; }

        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; } = null!;

        // Métodos de Dominio
        public void AcreditarDeposito(decimal monto)
        {
            if (monto <= 0)
                throw new ArgumentException("El monto debe ser mayor a cero.");

            SaldoTotal += monto;
            Version++;
        }

        public void RetenerSaldoPorPuja(decimal monto)
        {
            if (monto > SaldoDisponible)
                throw new InvalidOperationException("Saldo disponible insuficiente para realizar la puja.");

            SaldoRetenido += monto;
            Version++;
        }

        public void LiberarSaldoRetenido(decimal monto)
        {
            if (monto > SaldoRetenido)
                throw new InvalidOperationException("El monto a liberar es mayor al saldo retenido.");

            SaldoRetenido -= monto;
            Version++;
        }

        public void DebitarPorSubastaGanada(decimal monto)
        {
            SaldoRetenido -= monto;
            SaldoTotal -= monto;
            Version++;
        }
    }
}