using System;
using System.ComponentModel.DataAnnotations;

namespace PrisonApp.Models.ViewModels.HistoriaViewModel
{
    public class HistoriaZmianPrzydzialowViewModel
    {
        [Display(Name = "Numer Więźnia")]
        public int idWieznia { get; set; }

        [Display(Name = "Numer Celi")]
        public int? idCeli { get; set; }

        public string Pracownik { get; set; }

        public DateTime Data { get; set; }
    }
}