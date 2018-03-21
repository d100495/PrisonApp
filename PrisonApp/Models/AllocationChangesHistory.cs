namespace PrisonApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AllocationChangesHistory")]
    public partial class AllocationChangesHistory
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FK_AllocationChangesHistory_Allocation_Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FK_AllocationChangesHistory_ChangesHistory_Id { get; set; }

        public int? Cell_Id { get; set; }

        public virtual Allocation Allocation { get; set; }

        public virtual ChangesHistory ChangesHistory { get; set; }
    }
}
