using JuniorPharon.Models;


namespace JuniorPharon.ViewModels
{
    public static class MessageExt
    {
        public static Message ToCreate(this MessageCreateVM vm)
        {
            return new Message
            {
                Content = vm.Message,
                SenderId = vm.SenderId,
                ReceiverId = vm.ReceiverId,
                ChatId = vm.ChatId,
                SentAt = DateTime.Now
            };
        }
        public static MessageDetailsVM ToDetails(this Message message, string currentUserId)
        {
            return new MessageDetailsVM
            {
                Id = message.Id,
                Message = message.Content,
                SentAt = message.SentAt,
                SenderId = message.SenderId,
                ReceiverId = message.ReceiverId,
                ChatId = message.ChatId,
                SenderName = $"{message.Sender.FirstName} {message.Sender.LastName}",
                SenderImage = message.Sender.ProfileImg ?? "",
                ReceiverName = $"{message.Receiver.FirstName} {message.Receiver.LastName}",
                ReceiverImage = message.Receiver.ProfileImg ?? "",
                IsEdited = message.IsEdited,
                EditedAt = message.EditedAt ?? DateTime.MinValue,
            };
        }

        // Create Edit
        public static Message ToEdit(this MessageEditVM newmessage, Message oldmessage)
        {
            if (!string.IsNullOrWhiteSpace(newmessage.Content) && newmessage.Content != oldmessage.Content)
            {
                oldmessage.Content = newmessage.Content;
                oldmessage.IsEdited = true; // optional flag to track edited messages
                oldmessage.EditedAt = DateTime.Now; // optional timestamp
            }

            return oldmessage;
        }
    }
}
