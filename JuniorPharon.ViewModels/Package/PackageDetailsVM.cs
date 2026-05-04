namespace JuniorPharon.ViewModels
{
    public class PackageDetailsVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int DurationInDays { get; set; }

        public int MaxPeople { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }

        // 🔥 السعر (dynamic حسب عدد الأشخاص)
        public decimal PricePerPerson { get; set; }
        public decimal? OldPricePerPerson { get; set; }

        // 🔗 Trips
        public List<PackageTripDetailsVM> Trips { get; set; }

        // 💰 Pricing tiers (اختياري تعرضها)
        public List<PricingTierDetailsVM> PricingTiers { get; set; }
    }
}