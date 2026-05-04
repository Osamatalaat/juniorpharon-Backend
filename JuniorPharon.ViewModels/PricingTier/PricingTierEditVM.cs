using System.ComponentModel.DataAnnotations;

namespace JuniorPharon.ViewModels
{
    public class PricingTierEditVM
    {
        [Range(1, 1000)]
        public int? MinPeople { get; set; }

        [Range(1, 1000)]
        public int? MaxPeople { get; set; }

        [Range(0.01, 1000000)]
        public decimal? PricePerPerson { get; set; }

        [Range(0, 100)]
        public decimal? DiscountPercentage { get; set; }
    }
}