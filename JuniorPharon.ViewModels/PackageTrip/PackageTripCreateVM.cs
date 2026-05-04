using System.ComponentModel.DataAnnotations;

namespace JuniorPharon.ViewModels
{
    public class PackageTripCreateVM
    {
        [Required(ErrorMessage = "TripId is required.")]
        public int TripId { get; set; }

        [Required(ErrorMessage = "DayOrder is required.")]
        [Range(1, 365, ErrorMessage = "DayOrder must be at least 1.")]
        public int DayOrder { get; set; }
    }
}