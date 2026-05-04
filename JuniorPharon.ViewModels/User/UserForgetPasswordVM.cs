

using System.ComponentModel.DataAnnotations;

namespace JuniorPharon.ViewModels
{
    public class UserForgetPasswordVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Compare("Password")]

        public string NewPassword { get; set; }

        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string ResetToken { get; set; } // From email link // or Code
    }
}
