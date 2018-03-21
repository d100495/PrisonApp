using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrisonApplication.Models.ViewModels.HistoryViewModels
{
    public class AllocationHistoryViewModel
    {
        [Display(Name = "Prisoner Id")]
        public int Prisoner_Id { get; set; }

        [Display(Name = "Cell number")]
        public int? Cell_Id { get; set; }

        public string Worker { get; set; }

        public DateTime Date { get; set; }
    }
}