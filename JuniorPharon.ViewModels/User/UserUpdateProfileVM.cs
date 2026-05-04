

using JuniorPharon.Models.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace JuniorPharon.ViewModels
{
    public class UserUpdateProfileVM
    {
        [Required]
        public string Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? NationalId { get; set; }
        public string Nationality { get; set; }
        public int? Age { get; set; }
        public Gender? Gender { get; set; }
        public IFormFile? ProfileImg { get; set; }
        public string? ProfileImgUrl { get; set; }

    }
}
