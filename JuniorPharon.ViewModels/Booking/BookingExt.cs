using JuniorPharon.Models;

namespace JuniorPharon.ViewModels
{
    public static class BookingExt
    {
        // 🔹 Create
        public static Booking ToCreate(this BookingCreateVM vm)
        {
            return new Booking
            {
                TripId = vm.TripId,
                PackageId = vm.PackageId, // 🔥 مهم
                ClientId = vm.ClientId,
                StartDate = vm.StartDate,
                NumberOfPeople = vm.NumberOfPeople,
                Status = 0 // Pending
            };
        }

        // 🔹 Details
        public static BookingDetailsVM ToDetails(this Booking booking)
        {
            return new BookingDetailsVM
            {
                Id = booking.Id,
                BookDate = booking.BookDate,
                Status = booking.Status,
                NumberOfPeople = booking.NumberOfPeople,
                TotalPrice = booking.TotalPrice,
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,

                DurationInDays =
                    booking.Trip?.DurationInDays ??
                    booking.Package?.DurationInDays ?? 0,

                TripId = booking.TripId,
                PackageId = booking.PackageId, // 🔥 جديد

                TripName = booking.Trip != null
                    ? booking.Trip.Location
                    : null,

                PackageName = booking.Package != null
                    ? booking.Package.Name
                    : null,

                ClientId = booking.ClientId,

                ClientName = booking.Client != null
                    ? (booking.Client.FirstName + " " + booking.Client.LastName)
                    : string.Empty
            };
        }

        // 🔹 Edit
        public static Booking ToEdit(this BookingEditVM newModel, Booking oldModel)
        {
            if (newModel.StartDate != default)
                oldModel.StartDate = newModel.StartDate;

            if (newModel.NumberOfPeople > 0)
                oldModel.NumberOfPeople = newModel.NumberOfPeople;

            return oldModel;
        }
    }
}