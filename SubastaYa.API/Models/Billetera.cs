using System.ComponentModel.DataAnnotations;

namespace SubastaYa.API.Models
{
    public class Billetera
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public decimal SaldoTotal { get; set; }
        public decimal SaldoRetenido { get; set; }

        // Propiedad calculada
        public decimal SaldoDisponible => SaldoTotal - SaldoRetenido;

        // Clave para Optimistic Locking (Concurrencia)
        [ConcurrencyCheck] 
        public int Version { get; set; }

        // Relación orientada al Usuario
        public Usuario? Usuario { get; set; } 
    }
}
