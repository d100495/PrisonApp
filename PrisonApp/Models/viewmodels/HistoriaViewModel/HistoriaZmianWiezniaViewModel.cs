using System;
using System.ComponentModel.DataAnnotations;

namespace PrisonApp.Models.ViewModels
{
    public class HistoriaZmianWiezniaViewModel
    {
        [Display(Name = "Numer Więźnia")]
        public int idWieznia { get; set; }

        [Display(Name = "Imie więźnia")]
        public string ImieWieznia { get; set; }

        [Display(Name = "Nazwisko więźnia")]
        public string NazwiskoWieznia { get; set; }

        
        public string Pesel { get; set; }

        [Display(Name = "Płeć")]
        public string Plec { get; set; }

        public string Pracownik { get; set; }

        public DateTime Data { get; set; }
    }
}