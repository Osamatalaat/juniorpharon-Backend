using System.ComponentModel.DataAnnotations.Schema;
using JuniorPharon.Models.Enums;

namespace JuniorPharon.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public DateTime BookDate { get; set; } = DateTime.Now;

        public BookingStatus Status { get; set; }

        public int NumberOfPeople { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime? CancelDate { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        // ❌ نخليهم nullable
        public int? TripId { get; set; }
        public int? PackageId { get; set; }

        public string ClientId { get; set; }

        // 🔗 Relations
        public virtual Trip Trip { get; set; }
        public virtual Package Package { get; set; }
        public virtual User Client { get; set; }
        public virtual Payment Payment { get; set; }

        // ⚡ Calculated
        [NotMapped]
        public int DurationInDays =>
            Trip?.DurationInDays ?? Package?.DurationInDays ?? 0;

        [NotMapped]
        public DateTime? CalculatedEndDate =>
            StartDate.AddDays(DurationInDays);
    }
}