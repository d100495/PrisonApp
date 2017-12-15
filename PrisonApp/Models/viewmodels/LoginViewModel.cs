using System.ComponentModel.DataAnnotations;

namespace PrisonApp.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Pole Login jest wymagane")]
        [Display(Name = "Login")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Pole Hasło jest wymagane")]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Display(Name = "Zapamiętaj mnie")]
        public bool RememberMe { get; set; }
    }
}