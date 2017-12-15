using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace PrisonApp.Models
{
    [Table("Przydzialy")]
    public class Przydzialy
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Przydzialy()
        {
            HistoriaZmianPrzydzialow = new HashSet<HistoriaZmianPrzydzialow>();
        }

        [Display(Name = "Numer celi")]
        public int FK_idCeli { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FK_idWieznia { get; set; }

        public virtual Cele Cele { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HistoriaZmianPrzydzialow> HistoriaZmianPrzydzialow { get; set; }

        public virtual Wiezniowie Wiezniowie { get; set; }
    }
}