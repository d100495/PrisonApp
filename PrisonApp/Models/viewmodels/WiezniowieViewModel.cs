using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PrisonApp.Models.Validators;

namespace PrisonApp.Models.ViewModels
{
    public class WiezienViewModel
    {
        [Display(Name = "Imię Więźnia")]
        [Required(ErrorMessage = "Podaj Imię Więźnia")]
        [StringLength(45)]
        public string ImieWieznia { get; set; }

        [Display(Name = "Nazwisko Więźnia")]
        [Required(ErrorMessage = "Podaj Nazwisko Więźnia")]
        [StringLength(45)]
        public string NazwiskoWieznia { get; set; }


        [Required]
        [Display(Name = "Pesel")]
        [PeselValidator("Plec")]
        public string Pesel { get; set; }


        [Required(ErrorMessage = "Podaj płeć więźnia")]
        [RegularExpression("M|K", ErrorMessage = "Podaj prawidłowy format płci (M lub K)")]
        [Display(Name = "Płeć")]
        public string Plec { get; set; }

        [Required(ErrorMessage = "Podaj czas wyroku w miesiącach")]
        [Display(Name = "Czas wyroku")]
        [RegularExpression("^[0-9]{1,12}$", ErrorMessage = "Podaj prawidłowy czas wyroku")]
        public int Czas { get; set; }

        [Required(ErrorMessage = "Podaj date rozpoczęcia Wyroku (RRRR-MM-DD)")]
        [Display(Name = "Rozpoczęcie wyroku")]
        [Column(TypeName = "date")]
        [DataType(DataType.Date, ErrorMessage = "Podaj prawidłową date")]
        public DateTime DataRozpoczecia { get; set; }

        [Required(ErrorMessage = "Wybierz wyrok")]
        [Display(Name = "Kategoria Przestępstwa")]
        public int idKategoriiPrzestepstwa { get; set; }
    }
}