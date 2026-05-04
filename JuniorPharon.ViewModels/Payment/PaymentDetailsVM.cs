

using JuniorPharon.Models.Enums;

namespace JuniorPharon.ViewModels
{
    public class PaymentDetailsVM
    {
        public int Id { get; set; }
        public PaymentMethod Method { get; set; }  // Enum: Cash, Visa, etc.
        public bool IsDone { get; set; } = false;
        public decimal Amount { get; set; }  // What was paid
        public string Currency { get; set; } = "EGP";
        public int BookingId { get; set; } // FK to Booking
        public string ClientId { get; set; }  // Optional if you want to track who paid
        public string? ClientName { get; set; }   // Optional, display only
        public string? ClientImg { get; set; }    // Optional, display only
    }
}
