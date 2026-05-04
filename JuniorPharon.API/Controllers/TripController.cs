using JuniorPharon.Services;
using JuniorPharon.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace JuniorPharon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly TripService _tripService;

        public TripController(TripService tripService)
        {
            _tripService = tripService;
        }

        // ✅ Create Trip
        [HttpPost("CreateTrip")]
        public async Task<IActionResult> CreateTrip([FromForm] TripCreateVM vm)
        {
            var result = await _tripService.CreateTrip(vm);
            return new JsonResult(result);
        }

        // 🔍 Search Trip (🔥 updated)
        [HttpGet("SearchTrip")]
        public async Task<IActionResult> SearchTrip(
            [FromQuery] string? location = "",
            [FromQuery] string? city = "",
            [FromQuery] decimal? minPrice = null,   // 🔥 بدل float
            [FromQuery] decimal? maxPrice = null,   // 🔥 بدل float
            [FromQuery] int? durationInDays = null,
            [FromQuery] float? rating = null,
            [FromQuery] int peopleCount = 1,        // 🔥 أهم إضافة
            [FromQuery] bool descending = false,
            [FromQuery] int pageSize = 10,
            [FromQuery] int pageIndex = 1)
        {
            var result = await _tripService.SearchTrip(
                location,
                city,
                minPrice,
                maxPrice,
                durationInDays,
                rating,
                peopleCount,   // 👈 مهم
                descending,
                pageSize,
                pageIndex
            );

            return new JsonResult(result);
        }

        // 📦 Get All Trips (🔥 updated)
        [HttpGet("AllTrips")]
        public async Task<IActionResult> GetAllTrips(
            [FromQuery] int peopleCount = 1, // 🔥 مهم
            [FromQuery] int pageSize = 10,
            [FromQuery] int pageIndex = 1)
        {
            var result = await _tripService.GetAllTrips(peopleCount, pageSize, pageIndex);
            return new JsonResult(result);
        }

        // 🔎 Get Trip By Id (🔥 updated)
        [HttpGet("TripById")]
        public async Task<IActionResult> GetTripById(
            [FromQuery] int id,
            [FromQuery] int peopleCount = 1) // 🔥 مهم
        {
            var result = await _tripService.GetTripById(id, peopleCount);
            return new JsonResult(result);
        }
    }
}