namespace PrisonApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Allocation")]
    public partial class Allocation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Allocation()
        {
            AllocationChangesHistory = new HashSet<AllocationChangesHistory>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FK_Allocation_Prisoners_Id { get; set; }

        [Display(Name = "Cell number")]
        public int FK_Allocation_Cells_Id { get; set; }

        public virtual Cells Cells { get; set; }

        public virtual Prisoners Prisoners { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AllocationChangesHistory> AllocationChangesHistory { get; set; }
    }
}
