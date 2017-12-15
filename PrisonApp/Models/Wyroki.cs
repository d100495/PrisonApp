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

        [Required(ErrorMessage = "Podaj Katergorie przestępstwa")]
        [Display(Name = "Kategoria przestępstwa")]
        public int FK_idKategoriiPrzestepstwa { get; set; }

        [Required(ErrorMessage = "Podaj czas wyroku w miesiącach")]
        [Display(Name = "Czas wyroku")]
        [RegularExpression("^[0-9]{1,12}$", ErrorMessage = "Podaj prawidłowy czas wyroku")]
        public int Czas { get; set; }

        [Required(ErrorMessage = "Podaj date rozpoczęcia Wyroku (RRRR-MM-DD)")]
        [Display(Name = "Data rozpoczęcia wyroku")]
        [Column(TypeName = "date")]
        [DataType(DataType.Date, ErrorMessage = "Podaj prawidłową date")]
        public DateTime DataRozpoczecia { get; set; }

        public virtual KategoriePrzestepstwa KategoriePrzestepstwa { get; set; }

        public virtual Wiezniowie Wiezniowie { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HistoriaZmianWyrokow> HistoriaZmianWyrokow { get; set; }
    }
}