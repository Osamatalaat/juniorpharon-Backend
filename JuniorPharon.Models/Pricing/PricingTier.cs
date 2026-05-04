

namespace JuniorPharon.Models
{
    public class PricingTier
    {
        public int Id { get; set; }

        public int MinPeople { get; set; }
        public int MaxPeople { get; set; }

        public decimal PricePerPerson { get; set; }

        public decimal? DiscountPercentage { get; set; } // Admin discount

        public int? TripId { get; set; }
        public int? PackageId { get; set; }

        public virtual Trip Trip { get; set; }
        public virtual Package Package { get; set; }
    }
}
