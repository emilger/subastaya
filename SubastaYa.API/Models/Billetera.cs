using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SubastaYa.API.Models;

public class Billetera
{
    // Identificador de la tabla Billetera
    [Key]
    public int BilleteraId { get; set; }

    [Required]
    public int UsuarioId { get; set; }

    // Propiedades de la tabla Billetera
    public decimal SaldoTotal { get; set; }
    public decimal SaldoRetenido { get; set; }

    // Propiedad calculada en C# (Sin columna física en BD)
    public decimal SaldoDisponible { get; private set; }

    // Concurrencia para Optimistic Locking
    [ConcurrencyCheck]
    public int Version { get; set; }

    // Relación de navegación hacia Usuario
    [ForeignKey("UsuarioId")]
    public Usuario Usuario { get; set; }= null!;
}
