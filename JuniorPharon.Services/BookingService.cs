using JuniorPharon.Models.Enums;
using JuniorPharon.Repository;
using JuniorPharon.ViewModels;
using System;
using Utilities;



namespace JuniorPharon.Services
{
    public class BookingService
    {
        private readonly UnitOfWork _unitOfWork;

        public BookingService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult> CreateBooking(BookingCreateVM booking)
        {
            try
            {
                if (booking == null)
                    return ServiceResult.FailureResult("Invalid booking data.");
                await _unitOfWork._bookingRepository.AddAsync(booking.ToCreate());
                await _unitOfWork.SaveChangesAsync();
                return ServiceResult.SuccessResult("Booking created successfully.", System.Net.HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return ServiceResult.FailureResult($"An error occurred while creating the booking: {ex.Message}", System.Net.HttpStatusCode.InternalServerError);
            }
        }


        public async Task<ServiceResult<PaginationVM<BookingDetailsVM>>> SearchBooking 
            (
            string? userId = "",
            DateTime? _BookDate = null,
            DateTime? _StartDate = null,
            BookingStatus? status = null,
            bool descending = false,
           int pageSize = 10,
           int pageNumber = 1
            )
        {
            try
            {
                var bookings = await _unitOfWork._bookingRepository.SearchBookingDetails(userId, _BookDate, _StartDate, status, descending, pageSize, pageNumber);
                return ServiceResult<PaginationVM<BookingDetailsVM>>.SuccessResult(bookings, "Bookings retrieved successfully.", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ServiceResult<PaginationVM<BookingDetailsVM>>.FailureResult($"An error occurred while searching for bookings: {ex.Message}", System.Net.HttpStatusCode.InternalServerError);
            }
        }
        public async Task<ServiceResult<PaginationVM<BookingDetailsVM>>> GetAllBookings()
        {
            try
            {
                var bookings = await _unitOfWork._bookingRepository.GetAllBookingAsync();
               

                return ServiceResult<PaginationVM<BookingDetailsVM>>.SuccessResult(bookings, "Bookings retrieved successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return ServiceResult<PaginationVM<BookingDetailsVM>>.FailureResult(ex.Message);
            }
        }

        public async Task<ServiceResult<List<BookingDetailsVM>>> GetBookingsByClientId(string clientId)
        {
            try
            {
                var bookings =  _unitOfWork._bookingRepository
                    .GetList(b => b.ClientId == clientId);

                if (bookings == null || !bookings.Any())
                {
                    return ServiceResult<List<BookingDetailsVM>>
                        .FailureResult("No bookings found for this client.");
                }

                var result = bookings
                    .Select(b => b.ToDetails())
                    .ToList();

                return ServiceResult<List<BookingDetailsVM>>
                    .SuccessResult(result, "Bookings retrieved successfully.");
            }
            catch (Exception ex)
            {
                return ServiceResult<List<BookingDetailsVM>>
                    .FailureResult(ex.Message);
            }
        }

        public async Task<ServiceResult<BookingDetailsVM>> GetBookingById(string bookingId)
        {
            try
            {
                var booking = await _unitOfWork._bookingRepository.GetByIdAsync(bookingId);
                if (booking == null)
                {
                    return ServiceResult<BookingDetailsVM>
                        .FailureResult("Booking not found.");
                }
                return ServiceResult<BookingDetailsVM>
                    .SuccessResult(booking.ToDetails(), "Booking retrieved successfully.");
            }
            catch (Exception ex)
            {
                return ServiceResult<BookingDetailsVM>
                    .FailureResult(ex.Message);
            }
        }







    }
}
