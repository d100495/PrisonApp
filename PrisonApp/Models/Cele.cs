using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace PrisonApp.Models
{
    [Table("Cele")]
    public class Cele
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cele()
        {
            Przydzialy = new HashSet<Przydzialy>();
        }

        [Key]
        public int idCeli { get; set; }

        public int FK_idOdzial { get; set; }

        public bool Wolna { get; set; }

        public int IloscMiejsc { get; set; }

        [Column("Aktywna/Nieaktywna")]
        public bool Aktywna_Nieaktywna { get; set; }

        public virtual Odzial Odzial { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Przydzialy> Przydzialy { get; set; }
    }
}