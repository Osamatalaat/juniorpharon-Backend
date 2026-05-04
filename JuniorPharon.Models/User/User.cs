using JuniorPharon.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace JuniorPharon.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? NationalId { get; set; }
        public string Nationality { get; set; }
        public string? CurrentCountry { get; set; }
        public string? City { get; set; }
        public int? Age { get; set; }
        public string? ProfileImg { get; set; }
        public Gender? Gender { get; set; }
        //public Roles  Role { get; set; }
        public DateTime? ModificationDate { get; set; }
        public DateTime? CreationDate { get; set; }
        public bool? IsDeleted { get; set; }     
        public bool? IsActive { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

        // Relations
        public virtual ICollection<Trip> Trips { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Message> SentMessages { get; set; }
        public virtual ICollection<Message> ReceivedMessages { get; set; }
        public virtual ICollection<Notification> SentNotifications { get; set; }
        public virtual ICollection<Notification> ReceivedNotifications { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }

        public virtual Admin? Admin { get; set; }
        public virtual Client? Client { get; set; }
    }
}
