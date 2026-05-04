using JuniorPharon.Models;

namespace JuniorPharon.ViewModels
{
    public static class PackageTripExt
    {
        // 🔹 Create
        public static PackageTrip ToCreate(this PackageTripCreateVM vm)
        {
            return new PackageTrip
            {
                TripId = vm.TripId,
                DayOrder = vm.DayOrder
            };
        }

        // 🔹 Edit
        public static PackageTrip ToEdit(this PackageTripEditVM vm, PackageTrip model)
        {
            if (vm.DayOrder.HasValue)
                model.DayOrder = vm.DayOrder.Value;

            return model;
        }

        // 🔹 Details
        public static PackageTripDetailsVM ToDetails(this PackageTrip model)
        {
            return new PackageTripDetailsVM
            {
                TripId = model.TripId,
                DayOrder = model.DayOrder,

                // Optional Data
                TripLocation = model.Trip?.Location,
                TripCity = model.Trip?.City
            };
        }
    }
}