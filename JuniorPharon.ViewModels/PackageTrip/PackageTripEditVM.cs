using System.ComponentModel.DataAnnotations;

namespace JuniorPharon.ViewModels
{
    public class PackageTripEditVM
    {
        [Range(1, 365, ErrorMessage = "DayOrder must be at least 1.")]
        public int? DayOrder { get; set; }
    }
}