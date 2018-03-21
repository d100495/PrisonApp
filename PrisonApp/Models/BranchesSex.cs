namespace PrisonApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BranchesSex")]
    public partial class BranchesSex
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FK_BranchesSex_Branches_Id { get; set; }

        public int FK_BranchesSex_CategoriesOfCrimes_Id { get; set; }

        [Required]
        [StringLength(1)]
        public string Sex { get; set; }

        public virtual Branches Branches { get; set; }

        public virtual CategoriesOfCrimes CategoriesOfCrimes { get; set; }
    }
}
