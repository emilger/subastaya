using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SubastaYa.API.Models;

public class Billetera
{
    [Key]
    public int BilleteraId { get; set; }

    [Required]
    public int UsuarioId { get; set; }

    public decimal SaldoTotal { get; set; }
    public decimal SaldoRetenido { get; set; }

    // Propiedad calculada en C# (Sin columna física en BD)
    [NotMapped]
    public decimal SaldoDisponible { get; private set; }

    // Concurrencia para Optimistic Locking
    [ConcurrencyCheck]
    public int Version { get; set; }

    // Relación de navegación hacia Usuario
    public Usuario? Usuario { get; set; }
}
