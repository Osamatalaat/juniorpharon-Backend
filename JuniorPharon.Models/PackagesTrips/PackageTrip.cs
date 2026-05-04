

namespace JuniorPharon.Models
{
    public class PackageTrip
    {
        public int PackageId { get; set; }
        public int TripId { get; set; }

        public int DayOrder { get; set; } // ترتيب الرحلات جوه الباكج

        public virtual Package Package { get; set; }
        public virtual Trip Trip { get; set; }
    }
}
