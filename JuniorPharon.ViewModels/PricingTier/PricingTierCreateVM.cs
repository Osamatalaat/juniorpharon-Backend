using System.ComponentModel.DataAnnotations;

namespace JuniorPharon.ViewModels
{
    public class PricingTierCreateVM
    {
        [Required]
        [Range(1, 1000)]
        public int MinPeople { get; set; }

        [Required]
        [Range(1, 1000)]
        public int MaxPeople { get; set; }

        [Required]
        [Range(0.01, 1000000)]
        public decimal PricePerPerson { get; set; }

        [Range(0, 100)]
        public decimal? DiscountPercentage { get; set; }
    }
}