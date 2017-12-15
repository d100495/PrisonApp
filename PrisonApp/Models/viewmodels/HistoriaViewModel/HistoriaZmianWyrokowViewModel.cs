using System;
using System.ComponentModel.DataAnnotations;

namespace PrisonApp.Models.ViewModels.HistoriaViewModel
{
    public class HistoriaZmianWyrokowViewModel
    {
        [Display(Name = "Numer Więźnia")]
        public int idWieznia { get; set; }


        public string Pracownik { get; set; }

        public DateTime Data { get; set; }

        [Display(Name = "Data Rozpoczęcia")]
        public DateTime? DataRozpoczecia { get; set; }

        public int? Czas { get; set; }

        [Display(Name = "Nazwa Wyroku")]
        public string NazwaKategorii { get; set; }
    }
}