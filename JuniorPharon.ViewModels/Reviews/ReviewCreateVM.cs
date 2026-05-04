
namespace JuniorPharon.ViewModels
{
    public class ReviewCreateVM
    {
        public string Comment { get; set; }
        public float Rating { get; set; }

        public int TripId { get; set; }
        public string ClientId { get; set; }
    }
}
