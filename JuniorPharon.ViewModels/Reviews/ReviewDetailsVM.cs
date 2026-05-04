
namespace JuniorPharon.ViewModels
{
    public class ReviewDetailsVM
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public float Rating { get; set; }
        public DateTime CreationDate { get; set; }

        public int TripId { get; set; }
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string? ClientImage { get; set; }
    }
}
