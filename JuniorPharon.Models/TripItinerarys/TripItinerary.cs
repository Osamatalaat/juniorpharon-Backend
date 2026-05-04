namespace JuniorPharon.Models
{
    public class TripItinerary
    {
        public int Id { get; set; }

        public int DayNumber { get; set; } // اليوم (1,2,3...)

        public string Title { get; set; } // مثلا: Visit Pyramids

        public string Description { get; set; }

        public int TripId { get; set; }
        public virtual Trip Trip { get; set; }
    }
}