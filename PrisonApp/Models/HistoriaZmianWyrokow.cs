using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrisonApp.Models
{
    [Table("HistoriaZmianWyrokow")]
    public class HistoriaZmianWyrokow
    {
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idWieznia { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FK_idWyroku { get; set; }

        [Display(Name = "Przestêpstwo")]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idKategoriiPrzestepstwa { get; set; }

        public int? Czas { get; set; }

        [Display(Name = "Data rozpoczêcia wyroku")]
        [Column(TypeName = "date")]
        public DateTime? DataRozpoczecia { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FK_HistoriaZmian_idWpisu { get; set; }

        public virtual HistoriaZmian HistoriaZmian { get; set; }

        public virtual Wyroki Wyroki { get; set; }
    }
}