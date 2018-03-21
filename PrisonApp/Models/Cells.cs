namespace PrisonApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cells
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cells()
        {
            Allocation = new HashSet<Allocation>();
        }

        [Key]
        public int Cell_Id { get; set; }

        public bool IsEmpty { get; set; }

        public int Size { get; set; }

        public int FK_Cells_Branch { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Allocation> Allocation { get; set; }

        public virtual Branches Branches { get; set; }
    }
}
