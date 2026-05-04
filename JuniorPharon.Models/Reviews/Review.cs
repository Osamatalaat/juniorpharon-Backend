

namespace JuniorPharon.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public float Rating { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;

        public int? TripId { get; set; }
        public int? PackageId { get; set; }
        public virtual string ClientId { get; set; }

        public virtual Trip Trip { get; set; }
        public virtual Package Package { get; set; }
        public virtual User Client { get; set; }
    }
}
