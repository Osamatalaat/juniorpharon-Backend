using JuniorPharon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorPharon.ViewModels
{
    public static class ChatExt
    {
        public static Chat ToCreate(this ChatCreateVM chatCreateVM)
        {
            return new Chat
            {
                SenderId = chatCreateVM.SenderId,
                RecieverId = chatCreateVM.ReceiverId,
                IsActive = true,
                StartDate = DateTime.Now
            };
        }
        public static ChatDetailsVM ToDetails(this Chat chat, string _senderID, string _lastMessage, DateTime _messageDate)
        {
            return new ChatDetailsVM
            {
                Id = chat.Id,
                SenderID = chat.SenderId,
                ReceiverID = chat.RecieverId,
                IsActive = chat.IsActive,
                SenderImage = chat.Sender.ProfileImg ?? "",
                SenderName = chat.Sender.FirstName + " " + chat.Sender.LastName,
                ReceiverName = chat.Reciever.FirstName + " " + chat.Reciever.LastName,
                ReceiverImage = chat.Reciever.ProfileImg ?? "",
                LastMessage = _lastMessage,
                LastMessageDate = _messageDate,

            };
        }
    }
}
