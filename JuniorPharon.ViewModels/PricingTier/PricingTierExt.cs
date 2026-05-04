using JuniorPharon.Models;

namespace JuniorPharon.ViewModels
{
    public static class PricingTierExt
    {
        // 🔹 Create
        public static PricingTier ToCreate(this PricingTierCreateVM vm)
        {
            return new PricingTier
            {
                MinPeople = vm.MinPeople,
                MaxPeople = vm.MaxPeople,
                PricePerPerson = vm.PricePerPerson,
                DiscountPercentage = vm.DiscountPercentage
            };
        }

        // 🔹 Edit
        public static PricingTier ToEdit(this PricingTierEditVM vm, PricingTier model)
        {
            if (vm.MinPeople.HasValue)
                model.MinPeople = vm.MinPeople.Value;

            if (vm.MaxPeople.HasValue)
                model.MaxPeople = vm.MaxPeople.Value;

            if (vm.PricePerPerson.HasValue)
                model.PricePerPerson = vm.PricePerPerson.Value;

            if (vm.DiscountPercentage.HasValue)
                model.DiscountPercentage = vm.DiscountPercentage;

            return model;
        }

        // 🔹 Details
        public static PricingTierDetailsVM ToDetails(this PricingTier model)
        {
            var finalPrice = model.PricePerPerson;

            if (model.DiscountPercentage.HasValue)
            {
                finalPrice -= finalPrice * model.DiscountPercentage.Value / 100;
            }

            return new PricingTierDetailsVM
            {
                Id = model.Id,
                MinPeople = model.MinPeople,
                MaxPeople = model.MaxPeople,
                PricePerPerson = model.PricePerPerson,
                DiscountPercentage = model.DiscountPercentage,
                FinalPricePerPerson = finalPrice
            };
        }
    }
}