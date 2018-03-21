namespace PrisonApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CategoriesOfCrimes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CategoriesOfCrimes()
        {
            BranchesSex = new HashSet<BranchesSex>();
            Judgements = new HashSet<Judgements>();
        }

        [Key]
        public int CategoryOfCrime_Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Category")]
        public string NameOfCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BranchesSex> BranchesSex { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Judgements> Judgements { get; set; }
    }
}
