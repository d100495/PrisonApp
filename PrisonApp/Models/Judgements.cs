namespace PrisonApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Judgements
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Judgements()
        {
            JudgementsChangesHistory = new HashSet<JudgementsChangesHistory>();
        }

        [Key]
        public int Judgement_Id { get; set; }

        public int FK_Judgements_Prisoners_Id { get; set; }

        public int FK_Judgements_CategoriesOfCrimes_Id { get; set; }

        public int TimeOfJudgement { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        public virtual CategoriesOfCrimes CategoriesOfCrimes { get; set; }

        public virtual Prisoners Prisoners { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JudgementsChangesHistory> JudgementsChangesHistory { get; set; }
    }
}
