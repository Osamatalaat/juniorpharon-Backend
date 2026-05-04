using JuniorPharon.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace JuniorPharon.ViewModels
{
    public class PackageContentEditVM
    {
        [MaxLength(200)]
        public string? Name { get; set; }

        public string? Description { get; set; }

        public LanguageCode? LanguageCode { get; set; }
    }
}