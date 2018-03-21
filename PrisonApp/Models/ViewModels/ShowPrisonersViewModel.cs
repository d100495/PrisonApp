using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrisonApplication.Models.ViewModels
{
    public class ShowPrisonersViewModel
    {
        public Prisoners Prisoner { get; set; }
        public Judgements Judgement { get; set; }
        public Allocation Allocation { get; set; }
        [Display(Name = "Release Date")]
        public DateTime Date { get;set; }
    }
}