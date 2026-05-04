

using JuniorPharon.Models.Enums;

namespace JuniorPharon.ViewModels
{
    public class UserProfileVM
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? ProfileImg { get; set; }
        public string? NationalId { get; set; }
        public string? CurrentCountry { get; set; }
        public string? City { get; set; }
        public string Nationality { get; set; }
        public int? Age { get; set; }
        public Gender? Gender { get; set; }
        public Roles Role { get; set; }
        public DateTime? CreationDate { get; set; }
    }
}
