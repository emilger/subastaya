using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System;
namespace SubastaYa.API.Models
{
    public class TransaccionLedger
    {
        // identificadores de la tabla 
        [Key]
        public int TransaccionId { get; set; }
        [Required]
        public int BilleteraId { get; set; }
        public int? SubastaId { get; set; }

        // propiedades de la tabla
        [Required] 
        public string TipoTransaccion { get; set; } = string.Empty;
        [Required]
        public decimal Monto { get; set; }
        [Required] 
        public DateTime Fecha { get; set; } = DateTime.UtcNow;

        // relacion con la tabla Billetera
        [ForeignKey("BilleteraId")]
        public Billetera Billetera { get; set; } = null!;
        // relacion con la tabla Subasta 
        [ForeignKey("SubastaId")]
        public Subasta Subasta { get; set; } = null;


    }
}
