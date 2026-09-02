using Microsoft.EntityFrameworkCore;
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

        // Propiedad calculada
        public decimal SaldoDisponible { get; private set; }

        // Clave para Optimistic Locking (Concurrencia)
        [ConcurrencyCheck] 
        public int Version { get; set; }

        // Relación orientada al Usuario
        [ForeignKey("UsuarioId")]
        public Usuario? Usuario { get; set; } 
    }
}
