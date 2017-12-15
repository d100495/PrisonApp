using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrisonApp.Models
{
    [Table("KategoriaPrzestepstwaOdzial")]
    public class KategoriaPrzestepstwaOdzial
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FK_idOdzial { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FK_idKategoriiPrzestepstwa { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(1)]
        public string PlecOdzial { get; set; }

        public virtual KategoriePrzestepstwa KategoriePrzestepstwa { get; set; }

        public virtual Odzial Odzial { get; set; }
    }
}