

using System.ComponentModel.DataAnnotations;

namespace JuniorPharon.ViewModels
{
    public class UserLoginVM
    {
        [Required(ErrorMessage = "This Field is Required")]
        public string UserNameOrEmail { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RemeberMe { get; set; } = false;
        public string? ReturnUrl { get; set; } = "/";

    }
}
