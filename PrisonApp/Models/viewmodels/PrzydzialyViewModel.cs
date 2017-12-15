using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrisonApp.Models.ViewModels
{
    public class PrzydzialyViewModel
    {
        [Required(ErrorMessage = "Podaj numer celi")]
        [Display(Name = "Numer celi")]
        public int FK_idCeli { get; set; }

        [Key]
        public int idWieznia { get; set; }

        public int FK_idKategoriiPrzestepstwa { get; set; }

        public string Plec { get; set; }

        public IEnumerable<Cele> Cele { get; set; }
    }
}