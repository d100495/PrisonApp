using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrisonApp.Models
{
    [Table("HistoriaZmianWieznia")]
    public class HistoriaZmianWieznia
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FK_idWieznia { get; set; }

        [Display(Name = "Imie wi�nia")]
        [StringLength(45)]
        public string ImieWieznia { get; set; }

        [Display(Name = "Nazwisko wi�nia")]
        [StringLength(45)]
        public string NazwiskoWieznia { get; set; }


        [Column(Order = 1)]
        [StringLength(11)]
        public string Pesel { get; set; }

        [Display(Name = "P�e�")]
        [Column(Order = 2)]
        [StringLength(1)]
        public string Plec { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FK_HistoriaZmian_idWpisuWieznia { get; set; }

        public virtual HistoriaZmian HistoriaZmian { get; set; }

        public virtual Wiezniowie Wiezniowie { get; set; }
    }
}