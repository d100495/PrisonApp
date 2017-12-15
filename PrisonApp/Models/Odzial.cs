using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace PrisonApp.Models
{
    [Table("Odzial")]
    public class Odzial
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Odzial()
        {
            Cele = new HashSet<Cele>();
            KategoriaPrzestepstwaOdzial = new HashSet<KategoriaPrzestepstwaOdzial>();
        }

        [Key]
        public int idOdzial { get; set; }

        public int IloscCel { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cele> Cele { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KategoriaPrzestepstwaOdzial> KategoriaPrzestepstwaOdzial { get; set; }
    }
}