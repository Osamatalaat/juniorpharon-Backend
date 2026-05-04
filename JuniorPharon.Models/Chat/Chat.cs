namespace JuniorPharon.Models;

public class Chat
{
    public int Id { get; set; }
    public bool IsActive { get; set; }
    public DateTime StartDate { get; set; } = DateTime.Now;
    public string SenderId { get; set; }
    public string RecieverId { get; set; }
    public virtual User Sender { get; set; }
    public virtual User Reciever { get; set; }
    public virtual ICollection<Message> Messages { get; set; }
}