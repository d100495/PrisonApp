using System;
using System.ComponentModel.DataAnnotations;

namespace PrisonApp.Models.ViewModels
{
    public class WyswietlWiezniowViewModel
    {
        public Wiezniowie Wiezien { get; set; }
        public Wyroki Wyrok { get; set; }
        public Przydzialy Przydzial { get; set; }
        [Display(Name = "Data zakończenia")]
        public DateTime Date { get; set; }
    }
}