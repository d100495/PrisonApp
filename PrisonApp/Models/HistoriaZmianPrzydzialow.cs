using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrisonApp.Models
{
    [Table("HistoriaZmianPrzydzialow")]
    public class HistoriaZmianPrzydzialow
    {
        [Display(Name = "Numer Celi")]
        public int? idCeli { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FK_idWiezniaPrzydzialu { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FK_HistoriaZmian_idWpisuPrzydzialu { get; set; }

        public virtual HistoriaZmian HistoriaZmian { get; set; }

        public virtual Przydzialy Przydzialy { get; set; }
    }
}