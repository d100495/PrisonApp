using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using PrisonApplication.Models.Validators;

namespace PrisonApplication.Models.ViewModels
{
 
        public class PrisonerViewModel
        {
            [Display(Name = "Name")]
            public string PrisonerName { get; set; }

            [Display(Name = "Surname")]
            public string PrisonerSurname { get; set; }

            [PeselValidator("Sex")]
            public string Pesel { get; set; }

            public string Sex { get; set; }

            public int Time { get; set; }

            [Display(Name = "Start Date")]
            public DateTime StartDate { get; set; }
            
            [Display(Name = "Category of crime")]
            public int CategoryOfCrime_Id { get; set; }
        }
    }
  