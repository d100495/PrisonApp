using System;
using System.ComponentModel.DataAnnotations;

namespace PrisonApplication.Models.ViewModels.HistoryViewModels
{
    public class PrisonerHistoryViewModel
    {
        
            public int Prisoner_Id { get; set; }
            
            [Display(Name = "Name")]
            public string PrisonerName { get; set; }

            [Display(Name = "Surname")]
            public string PrisonerSurname { get; set; }

            public string Pesel { get; set; }

            public string Sex { get; set; }

            public string Worker { get; set; }

            public DateTime Date { get; set; }
        }
}
