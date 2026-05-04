
using JuniorPharon.Models;

namespace JuniorPharon.ViewModels
{
    public static class TripExt
    {
        
        public static Trip ToCreate(this TripCreateVM TripVm )
        {
            return new Trip
            { 
                //Price = TripVm.Price,
                DurationInDays = TripVm.DurationInDays,
                Location = TripVm.Location,
                City = TripVm.City,
                CreatedAt = DateTime.Now,
                CreatedBy = TripVm.CreatedByUserId,
                TripContents = TripVm.TripContents.Select(c => c.ToCreate()).ToList(),
                //TripImages = TripVm.TripPath.Select(p => new TripImage { ImageUrl = p }).ToList(),
                //TripImages = TripVm.TripImages.Select((imgVm , index)
                //=> imgVm.ToCreate(imgUrls[index])).ToList() 
              
            };
        }
        public static TripDetailsVM ToDetails(this Trip trip, int peopleCount)
        {
            var tier = trip.PricingTiers
                .FirstOrDefault(pt =>
                    pt.MinPeople <= peopleCount &&
                    pt.MaxPeople >= peopleCount
                );

            var price = tier?.PricePerPerson ?? 0;

            // 🔥 Apply Admin Discount
            if (tier?.DiscountPercentage != null)
            {
                price = price - (price * tier.DiscountPercentage.Value / 100);
            }

            return new TripDetailsVM
            {
                Id = trip.Id,
                Location = trip.Location,
                City = trip.City,
                DurationInDays = trip.DurationInDays,
                CreatedByUserId = trip.CreatedBy,
                CreatedAt = trip.CreatedAt,

                // ⭐ Rating
                Rating = trip.Reviews.Any()
                    ? trip.Reviews.Average(r => r.Rating)
                    : 0,

                // 💰 السعر الجديد
                PricePerPerson = price,

                // 💸 السعر القديم (قبل الخصم)
                OldPricePerPerson = tier?.PricePerPerson,

                TripContents = trip.TripContents.Select(c => c.ToDetails()).ToList(),
                TripImages = trip.TripImages.Select(img => img.ToDetails()).ToList()
            };
        }
    }
}
