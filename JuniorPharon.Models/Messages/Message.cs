

namespace JuniorPharon.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; } = DateTime.Now;
        public bool IsEdited { get; set; } = false; // Optional
        public DateTime? EditedAt { get; set; }

        //Relations
        public string SenderId { get; set; }
        public string ReceiverId { get; set; } 
        public virtual User Receiver { get; set; }
        public virtual User Sender { get; set; }
        public int ChatId { get; set; }
        public virtual Chat Chat { get; set; }
        
    }
}
