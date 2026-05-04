using JuniorPharon.Models.Enums;

namespace JuniorPharon.ViewModels
{
    public class BookingDetailsVM
    {
        public int Id { get; set; }

        public DateTime BookDate { get; set; }

        public BookingStatus Status { get; set; }

        public int NumberOfPeople { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int DurationInDays { get; set; }

        // 🔥 بدل Trip بس → نخلي الاتنين
        public int? TripId { get; set; }
        public int? PackageId { get; set; }

        // 🔥 Display
        public string? TripName { get; set; }
        public string? PackageName { get; set; }

        public string ClientId { get; set; }
        public string ClientName { get; set; }
    }
}