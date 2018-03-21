using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrisonApplication.Models.ViewModels
{
    public class EditJudgementViewModel
    {
        public int Prisoner_Id { get; set; }

        public Judgements Judgement { get; set; }
        
    }
}