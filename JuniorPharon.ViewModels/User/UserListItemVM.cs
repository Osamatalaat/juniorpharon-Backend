
using JuniorPharon.Models.Enums;

namespace JuniorPharon.ViewModels
{
    public class UserListItemVM
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public Roles Role { get; set; }
        public bool? IsActive { get; set; }
        public string? ProfileImg { get; set; }
    }
}
