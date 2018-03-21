namespace PrisonApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChangesHistory")]
    public partial class ChangesHistory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChangesHistory()
        {
            AllocationChangesHistory = new HashSet<AllocationChangesHistory>();
            JudgementsChangesHistory = new HashSet<JudgementsChangesHistory>();
            PrisonerChangesHistory = new HashSet<PrisonerChangesHistory>();
        }

        [Key]
        public int Record_Id { get; set; }

        [Required]
        public string Worker_Id { get; set; }

        public DateTime Date { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AllocationChangesHistory> AllocationChangesHistory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JudgementsChangesHistory> JudgementsChangesHistory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PrisonerChangesHistory> PrisonerChangesHistory { get; set; }
    }
}
