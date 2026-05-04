using JuniorPharon.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace JuniorPharon.ViewModels
{
    public class PackageContentCreateVM
    {
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Language is required.")]
        public LanguageCode LanguageCode { get; set; }
    }
}