using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace PrisonApp.Models
{
    [Table("KategoriePrzestepstwa")]
    public class KategoriePrzestepstwa
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KategoriePrzestepstwa()
        {
            KategoriaPrzestepstwaOdzial = new HashSet<KategoriaPrzestepstwaOdzial>();
            Wyroki = new HashSet<Wyroki>();
        }

        [Key]
        public int idKategoriiPrzestepstwa { get; set; }

        [Display(Name = "Nazwa kategorii")]
        [Required]
        [StringLength(45)]
        public string NazwaKategorii { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KategoriaPrzestepstwaOdzial> KategoriaPrzestepstwaOdzial { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Wyroki> Wyroki { get; set; }
    }
}