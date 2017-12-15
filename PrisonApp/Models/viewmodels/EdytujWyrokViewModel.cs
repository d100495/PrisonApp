using System.ComponentModel.DataAnnotations;

namespace PrisonApp.Models.ViewModels
{
    public class EdytujWyrokViewModel
    {
        [Key]
        public int idWieznia { get; set; }

        public Wyroki Wyrok { get; set; }
        //public int FK_idWieznia { get; set; }

        //   public string Plec { get; set; }

        //[Key]
        //public int idWyroku { get; set; }

        //public int FK_idKategoriiPrzestepstwa { get; set; }

        //public int Czas { get; set; }

        //[Column(TypeName = "date")]
        //public DateTime DataRozpoczecia { get; set; }
    }
}