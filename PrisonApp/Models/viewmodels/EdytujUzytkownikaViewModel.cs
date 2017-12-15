using System.ComponentModel.DataAnnotations;
using PrisonApp.Models.Validators;

namespace PrisonApp.Models.ViewModels
{
    public class EdytujUzytkownikaViewModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Nazwa Użytkownika")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Imię")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        [PeselValidator("Plec")]
        [Required]
        [Display(Name = "Pesel")]
        public string Pesel { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Podaj email")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}