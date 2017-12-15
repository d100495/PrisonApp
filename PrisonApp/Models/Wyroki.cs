using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace PrisonApp.Models
{
    [Table("Wyroki")]
    public class Wyroki
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Wyroki()
        {
            HistoriaZmianWyrokow = new HashSet<HistoriaZmianWyrokow>();
        }

        public int FK_idWieznia { get; set; }

        [Key]
        public int idWyroku { get; set; }

        [Required(ErrorMessage = "Podaj Katergorie przestêpstwa")]
        [Display(Name = "Kategoria przestêpstwa")]
        public int FK_idKategoriiPrzestepstwa { get; set; }

        [Required(ErrorMessage = "Podaj czas wyroku w miesi¹cach")]
        [Display(Name = "Czas wyroku")]
        [RegularExpression("^[0-9]{1,12}$", ErrorMessage = "Podaj prawid³owy czas wyroku")]
        public int Czas { get; set; }

        [Required(ErrorMessage = "Podaj date rozpoczêcia Wyroku (RRRR-MM-DD)")]
        [Display(Name = "Data rozpoczêcia wyroku")]
        [Column(TypeName = "date")]
        [DataType(DataType.Date, ErrorMessage = "Podaj prawid³ow¹ date")]
        public DateTime DataRozpoczecia { get; set; }

        public virtual KategoriePrzestepstwa KategoriePrzestepstwa { get; set; }

        public virtual Wiezniowie Wiezniowie { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HistoriaZmianWyrokow> HistoriaZmianWyrokow { get; set; }
    }
}