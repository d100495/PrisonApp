using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrisonApplication.Models.ViewModels
{
    public class AllocationViewModel
    {           
        public int Cell_Id { get; set; }

        public int Prisoner_Id { get; set; }

        public int CategoryOfCrime_Id { get; set; }

        public string Sex { get; set; }

        public IEnumerable<Cells> Cells { get; set; }
    }
}