

using System.ComponentModel.DataAnnotations;

namespace JuniorPharon.ViewModels
{
    public class UserChangePasswordVM
    {
        [Required, DataType(DataType.Password)]

        public string CurrentPassword { get; set; }

        [Required, DataType(DataType.Password)]
        [Compare("Password")]

        public string NewPassword { get; set; }

        [Required, DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }
    }
}
