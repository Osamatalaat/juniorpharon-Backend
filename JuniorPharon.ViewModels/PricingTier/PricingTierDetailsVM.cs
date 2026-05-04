namespace JuniorPharon.ViewModels
{
    public class PricingTierDetailsVM
    {
        public int Id { get; set; }

        public int MinPeople { get; set; }
        public int MaxPeople { get; set; }

        public decimal PricePerPerson { get; set; }

        public decimal? DiscountPercentage { get; set; }

        // 🔥 محسوب
        public decimal FinalPricePerPerson { get; set; }
    }
}