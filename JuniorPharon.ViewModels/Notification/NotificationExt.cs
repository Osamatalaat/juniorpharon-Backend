

using JuniorPharon.Models;

namespace JuniorPharon.ViewModels
{
    public static class NotificationExt
    {
        public static Notification ToCreate(this NotificationCreateVM vm)
        {
            return new Notification
            {
                Title = vm.Title,
                Description = vm.Description,
                Type = vm.Type,
                ReceiverId = vm.RecieverId,
                SenderId = vm.SenderId,
                CreatedAt = DateTime.Now,
                IsRead = false
            };
        }
        // Model → DetailsVM
        public static NotificationDetailsVM ToDetails(this Notification model)
        {
            return new NotificationDetailsVM
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                Type = model.Type,
                IsRead = model.IsRead,
                CreatedAt = model.CreatedAt,
                RecieverId = model.ReceiverId,
                SenderId = model.SenderId,
                SenderName = model.Sender != null ? model.Sender.FirstName + " " + model.Sender.LastName : null,
                SenderImage = model.Sender?.ProfileImg ?? null
            };
        }
    }
}
