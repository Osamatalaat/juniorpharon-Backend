

using JuniorPharon.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace JuniorPharon.ViewModels
{
    public class PaymentCreateVM
    {
        public PaymentMethod Method { get; set; }  // Enum: Cash, Visa, etc.
        public int BookingId { get; set; } // FK to Booking
        public string? ClientId { get; set; }  // Optional if you want to track who paid
        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string Currency { get; set; } = "EGP"; // Default currency, editable

        public string? TransactionId { get; set; }



    }
}
