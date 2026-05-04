

using JuniorPharon.Models.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace JuniorPharon.ViewModels
{
    public class UserRegisterVM
    {
        [Required(ErrorMessage = "First Name is Required")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Value Must between 2 and 20 characters ")]
        public string FirstName { get; set; }
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Country Code is required")]
        public string CountryCode { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "UserName is Required")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Value Must be at least 5 characters ")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Nationality is Required")]
        public string Nationality { get; set; }

        public string? ProfileImg { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile? ImageFile { get; set; }

        public string? CurrentCountry { get; set; }
        public string? City { get; set; }
        public int? Age { get; set; }

        [Required(ErrorMessage = "Your gender is Required")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Your Email is Required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Value Must be at least 8 Characters ")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword is Required")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public string Role { get; set; } 
    }
}
