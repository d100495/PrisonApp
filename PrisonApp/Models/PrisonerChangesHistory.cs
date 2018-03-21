namespace PrisonApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PrisonerChangesHistory")]
    public partial class PrisonerChangesHistory
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FK_PrisonerChangesHistory_Prisoner_Id { get; set; }

        [StringLength(11)]
        public string Pesel { get; set; }

        [StringLength(1)]
        public string Sex { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FK_PrisonerChangesHistory_ChangesHistory_Id { get; set; }

        [StringLength(45)]
        public string PrisonerName { get; set; }

        [StringLength(45)]
        public string PrisonerSurname { get; set; }

        public virtual ChangesHistory ChangesHistory { get; set; }

        public virtual Prisoners Prisoners { get; set; }
    }
}
