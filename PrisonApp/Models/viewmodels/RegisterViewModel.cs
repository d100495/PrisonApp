using System.ComponentModel.DataAnnotations;

namespace PrisonApplication.Models.ViewModels
{
    public class RegisterViewModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Match error")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        
        [Display(Name = "Type of account")]
        public string Role { get; set; }
    }
}