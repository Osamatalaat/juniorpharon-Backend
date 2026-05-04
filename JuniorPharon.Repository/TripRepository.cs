using Infrastructure.SqlServer;
using JuniorPharon.Models;
using JuniorPharon.ViewModels;
using LinqKit;

namespace JuniorPharon.Repository
{
    public class TripRepository : BaseRepository<Trip>
    {
        public TripRepository(DBContext context) : base(context)
        {
        }

        public async Task<PaginationVM<TripDetailsVM>> SearchTripDetails(
            string? location = "",
            string? city = "",
            decimal? minPrice = null,
            decimal? maxPrice = null,
            int? durationInDays = null,
            float? rating = null,
            int peopleCount = 1, // 🔥 مهم
            bool descending = false,
            int pageSize = 10,
            int pageIndex = 1)
        {
            try
            {
                var predicate = PredicateBuilder.New<Trip>(true);

                // 🔍 Filter: Location
                if (!string.IsNullOrEmpty(location))
                    predicate = predicate.And(t => t.Location.Contains(location));

                // 🔍 Filter: City
                if (!string.IsNullOrEmpty(city))
                    predicate = predicate.And(t => t.City.Contains(city));

                // 💰 Filter: Price (من PricingTier)
                if (minPrice.HasValue)
                {
                    predicate = predicate.And(t =>
                        t.PricingTiers.Any(pt =>
                            pt.MinPeople <= peopleCount &&
                            pt.MaxPeople >= peopleCount &&
                            pt.PricePerPerson >= minPrice.Value
                        ));
                }

                if (maxPrice.HasValue)
                {
                    predicate = predicate.And(t =>
                        t.PricingTiers.Any(pt =>
                            pt.MinPeople <= peopleCount &&
                            pt.MaxPeople >= peopleCount &&
                            pt.PricePerPerson <= maxPrice.Value
                        ));
                }

                // ⏱ Duration
                if (durationInDays.HasValue)
                    predicate = predicate.And(t => t.DurationInDays == durationInDays.Value);

                // ⭐ Rating
                if (rating.HasValue)
                {
                    predicate = predicate.And(t =>
                        t.Reviews.Any() &&
                        t.Reviews.Average(r => r.Rating) >= rating.Value
                    );
                }

                // 🚀 Execute
                return await SearchAsync(
                    predicate,
                    t => t.CreatedAt,                    // Order By
                    t => t.ToDetails(peopleCount),       // Projection 👈 مهم
                    descending,
                    pageSize,
                    pageIndex
                );
            }
            catch
            {
                throw;
            }
        }

        // 📦 Get All Trips
        public async Task<PaginationVM<TripDetailsVM>> GetAllTripsAsync(
            int peopleCount = 1, // 🔥 مهم
            int pageSize = 10,
            int pageIndex = 1)
        {
            try
            {
                return await SearchAsync(
                    null,
                    t => t.CreatedAt,
                    t => t.ToDetails(peopleCount), // 👈 مهم
                    false,
                    pageSize,
                    pageIndex
                );
            }
            catch
            {
                throw;
            }
        }
    }
}