using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
namespace SubastaYa.API.Models
{
    public class Puja
    {
        // identificadores de puja
        [Key]
        public int PujaId { get; set; }
        [Required]
        public int SubastaId { get; set; }
        [Required]
        public int CompradorId { get; set; }

        // atributos de la puja
        [Required]
        public DataSetDateTime FechaPuja { get; set; } = new DataSetDateTime();
        [Required] 
        public int MontoPuja { get; set; }

        // Relación orientada a la Subasta
        public ICollection<Subasta> IntentoPuja { get; set; } = new List<Subasta>();
        //relación orientada al Comprador
        public ICollection<Usuario> Comprador { get; set; } = new List<Usuario>();

    }
}
