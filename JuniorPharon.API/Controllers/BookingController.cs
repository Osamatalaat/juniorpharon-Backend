using JuniorPharon.Models.Enums;
using JuniorPharon.Services;
using JuniorPharon.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JuniorPharon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly BookingService bookingService;
        public BookingController(BookingService bookingService)
        {
            this.bookingService = bookingService;
        }


        [HttpPost("CreateBooking")]
        public async Task<IActionResult> CreateBooking([FromBody] BookingCreateVM booking)
        {
            var result = await bookingService.CreateBooking(booking);
            if (result.IsSuccess)
            {
                return new JsonResult(result);
            }
            return new JsonResult(result);
        }

        [HttpGet("SearchBooking")]
        public async Task<IActionResult> SearchBooking(
            [FromQuery] string? userId = "",
            [FromQuery] DateTime? _BookDate = null,
            [FromQuery] DateTime? _StartDate = null,
            [FromQuery] BookingStatus? status = null,
            [FromQuery] bool descending = false,
            [FromQuery] int pageSize = 10,
            [FromQuery] int pageNumber = 1)
        {
            var result = await bookingService.SearchBooking(userId, _BookDate, _StartDate, status, descending, pageSize, pageNumber);
            if (result.IsSuccess)
            {
                return new JsonResult(result);
            }
            return new JsonResult(result);
        }

        [HttpGet("AllBookings")]
        public async Task<IActionResult> GetAllBookings()
        {
            var result = await bookingService.GetAllBookings();
            if (result.IsSuccess)
            {
                return new JsonResult(result);
            }
            return new JsonResult(result);
        }

        [HttpGet("BookingByClientId")]

        public async Task<IActionResult> GetBookingsByClientId(string clientId)
        {
            var result = await bookingService.GetBookingsByClientId(clientId);
            if (result.IsSuccess)
            {
                return new JsonResult(result);
            }
            return new JsonResult(result);
        }



        [HttpGet("BookingByBookingId")]

        public async Task<IActionResult> GetBookingsByBookingId(string bookingId)
        {
            var result = await bookingService.GetBookingById(bookingId);
            if (result.IsSuccess)
            {
                return new JsonResult(result);
            }
            return new JsonResult(result);
        }




    }
    }

