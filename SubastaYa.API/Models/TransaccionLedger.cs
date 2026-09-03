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
        public int BilleteraFk { get; set; }
        public int? SubastaFk { get; set; }

        // propiedades de la tabla
        [Required] 
        public string TipoTransaccion { get; set; } = string.Empty;
        [Required]
        public decimal Monto { get; set; }
        [Required] 
        public DateTime fecha { get; set; } = DateTime.UtcNow;

        // relacion con la tabla Billetera
        [ForeignKey("BilleteraFk")]
        public Billetera? Billetera { get; set; } = null!;
        // relacion con la tabla Subasta 
        [ForeignKey("SubastaFk")]
        public Subasta? Subasta { get; set; } = null!;


    }
}
