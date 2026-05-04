
namespace JuniorPharon.Models
{
    public class TripImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public int TripId { get; set; }
        public bool IsCover { get; set; }
        
        public virtual Trip Trip { get; set; }
    }
}
