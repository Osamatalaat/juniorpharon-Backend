

using JuniorPharon.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace JuniorPharon.ViewModels
{
    public class NotificationCreateVM
    {
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Type is required.")]
        public NotificationType Type { get; set; }

        [Required(ErrorMessage = "UserId is required.")]
        public string RecieverId { get; set; }
        public string? SenderId { get; set; }

    }
}
