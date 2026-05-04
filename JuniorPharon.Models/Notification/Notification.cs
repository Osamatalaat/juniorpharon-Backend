using JuniorPharon.Models.Enums;

namespace JuniorPharon.Models;

public class Notification
{
    public int Id { get; set; }
    public string Description { get; set; }
    public string Title { get; set; }
    public NotificationType  Type { get; set; }
    public bool IsRead { get; set; } = false; // Optional, default to false
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    //Relations
    public string? SenderId { get; set; }
    public string ReceiverId { get; set; }
    public virtual User Receiver { get; set; }
    public virtual User? Sender { get; set; }

}