
using System.ComponentModel.DataAnnotations;


namespace JuniorPharon.ViewModels
{
    public class ChatDetailsVM
    {
        public int Id { get; set; }
        public string SenderID { get; set; }
        public string ReceiverID { get; set; }
        [Required, DataType(DataType.DateTime)]
        public DateTime LastMessageDate { get; set; }
        public DateTime LastSeen { get; set; } // logic in service to update this field
        public string LastMessage { get; set; }
        public bool IsActive { get; set; }
        public string SenderImage { get; set; }
        public string ReceiverImage { get; set; }
        public string SenderName { get; set; }
        public string ReceiverName { get; set; }
    }
}
