using System.ComponentModel.DataAnnotations;

namespace PrisonApp.Models.ViewModels
{
    public class RegisterViewModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Nazwa Użytkownika")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane")]
        [StringLength(255, ErrorMessage = "Hasło musi posiadać minimum 6 znaków oraz 1 cyfrę", MinimumLength = 6)]
        [RegularExpression(".*[0-9].*", ErrorMessage = "Hasło musi posiadać minimum 6 znaków oraz 1 cyfrę")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Hasło oraz potwierdzenie hasła nie pasują do siebie")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Imię")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "Pesel")]
        public string Pesel { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Podaj email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Musisz wybrać typ konta")]
        [Display(Name = "Typ konta")]
        public string Role { get; set; }
    }
}