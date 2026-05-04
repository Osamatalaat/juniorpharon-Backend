using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorPharon.ViewModels
{
    public class MessageDetailsVM
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string Message { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsEdited { get; set; }
        public DateTime EditedAt { get; set; }
        public string SenderName { get; set; }
        public string SenderImage { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverImage { get; set; }
        public int ChatId { get; set; }
    }
}
