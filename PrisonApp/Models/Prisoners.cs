using PrisonApplication.Models.Validators;

namespace PrisonApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Prisoners
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Prisoners()
        {
            Judgements = new HashSet<Judgements>();
            PrisonerChangesHistory = new HashSet<PrisonerChangesHistory>();
        }

        [Key]
        
        public int Prisoner_Id { get; set; }

        [StringLength(50)]
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required")]
        public string PrisonerName { get; set; }

        [Display(Name = "Surname")]
        [Required(ErrorMessage = "Surname is required")]
        [StringLength(50)]
        public string PrisonerSurname { get; set; }

        [Required(ErrorMessage = "Pesel is required")]
        [StringLength(11)]
        [PeselValidator("Sex")]
        public string Pesel { get; set; }

        [Required(ErrorMessage = "Sex is Required")]
        [StringLength(1)]
        public string Sex { get; set; }

        public virtual Allocation Allocation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Judgements> Judgements { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PrisonerChangesHistory> PrisonerChangesHistory { get; set; }
    }
}
