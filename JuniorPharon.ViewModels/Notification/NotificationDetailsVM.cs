

using JuniorPharon.Models.Enums;

namespace JuniorPharon.ViewModels
{
    public class NotificationDetailsVM
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public NotificationType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
        public string RecieverId { get; set; }
        public string? SenderId { get; set; }
        public string? SenderName { get; set; }
        public string? SenderImage { get; set; }
    }
}
