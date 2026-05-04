

using JuniorPharon.Models;

namespace JuniorPharon.ViewModels
{
    public static class ReviewExt
    {
        public static Review ToCreate(this ReviewCreateVM vm)
        {
            return new Review
            {
                Comment = vm.Comment,
                Rating = vm.Rating,
                TripId = vm.TripId,
                ClientId = vm.ClientId,
                CreationDate = DateTime.Now
            };
        }

        // Model → Details
        public static ReviewDetailsVM ToDetails(this Review model)
        {
            return new ReviewDetailsVM
            {
                Id = model.Id,
                Comment = model.Comment,
                Rating = model.Rating,
                CreationDate = model.CreationDate,
                //TripId = model.TripId,
                ClientId = model.ClientId,
                ClientName = model.Client?.FirstName + " " + model.Client?.LastName,
                ClientImage = model.Client?.ProfileImg ?? null,
            };
        }

        // Edit → Model update
        public static void UpdateFromEditVM(this Review model, ReviewEditVM vm)
        {
            model.Comment = vm.Comment;
            model.Rating = vm.Rating;
        }
    }
}
