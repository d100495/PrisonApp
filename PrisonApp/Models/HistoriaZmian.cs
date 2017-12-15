using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace PrisonApp.Models
{
    [Table("HistoriaZmian")]
    public class HistoriaZmian
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HistoriaZmian()
        {
            HistoriaZmianPrzydzialow = new HashSet<HistoriaZmianPrzydzialow>();
            HistoriaZmianWieznia = new HashSet<HistoriaZmianWieznia>();
            HistoriaZmianWyrokow = new HashSet<HistoriaZmianWyrokow>();
        }

        [Key]
        public int idWpisu { get; set; }

        public string idPracownika { get; set; }

        [Column(TypeName = "date")]
        public DateTime Data { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HistoriaZmianPrzydzialow> HistoriaZmianPrzydzialow { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HistoriaZmianWieznia> HistoriaZmianWieznia { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HistoriaZmianWyrokow> HistoriaZmianWyrokow { get; set; }
    }
}