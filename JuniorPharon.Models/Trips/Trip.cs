

namespace JuniorPharon.Models
{
    public class Trip
    {
        public int Id { get; set; }
        
        public string Location { get; set; }

        public string City { get; set; }
        //public float Price { get; set; }
        
        
        public int DurationInDays { get; set; } // trip duration
        public string CreatedBy { get; set; }//fk owner
        public DateTime CreatedAt { get; set; }

        //public string ImgCover { get; set; }
        
        //Relation
        
        public virtual User CreatedByUser { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<TripImage> TripImages { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<TripContent> TripContents { get; set; } // for multilingual support
        public virtual ICollection<PricingTier> PricingTiers { get; set; }
        public virtual ICollection<TripItinerary> Itineraries { get; set; }

    }
}
