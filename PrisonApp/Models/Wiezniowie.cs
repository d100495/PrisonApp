using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using PrisonApp.Models.Validators;

namespace PrisonApp.Models
{
    [Table("Wiezniowie")]
    public class Wiezniowie
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Wiezniowie()
        {
            HistoriaZmianWieznia = new HashSet<HistoriaZmianWieznia>();
            Wyroki = new HashSet<Wyroki>();
        }

        [Key]
        public int idWieznia { get; set; }

        [Display(Name = "Imi� Wi�nia")]
        [Required(ErrorMessage = "Podaj Imi� Wi�nia")]
        [StringLength(45)]
        public string ImieWieznia { get; set; }

        [Display(Name = "Nazwisko Wi�nia")]
        [Required(ErrorMessage = "Podaj Nazwisko Wi�nia")]
        [StringLength(45)]
        public string NazwiskoWieznia { get; set; }


        [Required(ErrorMessage = "Podaj Pesel")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Podaj prawidlowy numer pesel")]
        [PeselValidator("Plec")]
        public string Pesel { get; set; }

        [Required(ErrorMessage = "Podaj p�e� wi�nia")]
        [StringLength(1, MinimumLength = 1, ErrorMessage = "Podaj prawid�owy format p�ci (M lub K)")]
        [Display(Name = "P�e�")]
        public string Plec { get; set; }

        public virtual Przydzialy Przydzialy { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HistoriaZmianWieznia> HistoriaZmianWieznia { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Wyroki> Wyroki { get; set; }
    }
}