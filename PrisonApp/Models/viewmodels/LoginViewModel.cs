

using System.ComponentModel.DataAnnotations;

namespace PrisonApplication.Models.ViewModels
{
    public class LoginViewModel
    {
        
        [Display(Name = "Login")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}