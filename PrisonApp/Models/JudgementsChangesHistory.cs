namespace PrisonApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("JudgementsChangesHistory")]
    public partial class JudgementsChangesHistory
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Prisoner_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FK_JudgementsChangesHistory_Judgements { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CategoryOfCrimes_Id { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FK_JudgementsChangesHistory_ChangesHistory_Id { get; set; }

        public int? Time { get; set; }

        public DateTime? Date { get; set; }

        public virtual ChangesHistory ChangesHistory { get; set; }

        public virtual Judgements Judgements { get; set; }
    }
}
