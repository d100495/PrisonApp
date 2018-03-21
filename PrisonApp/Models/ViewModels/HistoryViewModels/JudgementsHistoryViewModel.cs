using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrisonApplication.Models.ViewModels.HistoryViewModels
{
    public class JudgementsHistoryViewModel
    {
        [Display(Name = "Prisoner Id")]
        public int Prisoner_Id { get; set; }

        public string Worker { get; set; }

        public DateTime Date { get; set; }

         [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Time Of Judgement")]
        public int? TimeOfJudgement { get; set; }

        [Display(Name = "Category name")]
        public string NameOfCategory { get; set; }
    }
}