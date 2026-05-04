namespace JuniorPharon.ViewModels
{
    public class PackageTripDetailsVM
    {
        public int TripId { get; set; }
        public int DayOrder { get; set; }

        // Optional (لو عايز تعرض بيانات التريب)
        public string? TripLocation { get; set; }
        public string? TripCity { get; set; }
    }
}