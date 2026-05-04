using JuniorPharon.Models;
using JuniorPharon.Repository;
using JuniorPharon.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace JuniorPharon.Services
{
    public class TripService
    {
        private readonly UnitOfWork _unitOfWork;

        public TripService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult> CreateTrip([FromForm] TripCreateVM vm)
        {
            try
            {
                if (vm == null)
                    return ServiceResult.FailureResult("Invalid Trip data.");

                var trip = vm.ToCreate();
                // ✅ إنشاء Student Payment
                foreach (var imgVm in vm.TripImages)
                {
                    var imageUrl = await UploadMedia.AddImageAsync(imgVm.Image);

                    trip.TripImages.Add(imgVm.ToCreate(imageUrl));

                }

                await _unitOfWork._tripRepository.AddAsync(trip);
                await _unitOfWork.SaveChangesAsync();

                return ServiceResult.SuccessResult("Trip created successfully.", HttpStatusCode.Created);
            }

            catch (Exception ex)
            {
                return ServiceResult.FailureResult($"An error occurred while creating the trip: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ServiceResult<PaginationVM<TripDetailsVM>>> SearchTrip(
        string? location = "",
        string? city = "",
        decimal? minPrice = null,
        decimal? maxPrice = null,
        int? durationInDays = null,
        float? rating = null,
        int peopleCount = 1, // 🔥 ضيف ده
        bool descending = false,
        int pageSize = 10,
        int pageIndex = 1)
        {
            try
            {
                var trips = await _unitOfWork._tripRepository.SearchTripDetails(
                    location, city, minPrice, maxPrice, durationInDays, rating, peopleCount, descending, pageSize, pageIndex);

                return ServiceResult<PaginationVM<TripDetailsVM>>.SuccessResult(trips, "Trips retrieved successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return ServiceResult<PaginationVM<TripDetailsVM>>.FailureResult(ex.Message);
            }
        }

        public async Task<ServiceResult<PaginationVM<TripDetailsVM>>> GetAllTrips(int pageSize,int pageIndex , int peopleCount = 1)
        {
            try
            {
                var trips = await _unitOfWork._tripRepository.GetAllTripsAsync(peopleCount,pageSize, pageIndex );

                return ServiceResult<PaginationVM<TripDetailsVM>>.SuccessResult(trips, "Enrollments retrieved successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return ServiceResult<PaginationVM<TripDetailsVM>>.FailureResult(ex.Message);
            }
        }

        public async Task<ServiceResult<TripDetailsVM>> GetTripById(int id , int peopleCount =1)
        {
            try
            {
                var trip = _unitOfWork._tripRepository
                    .GetList(e => e.Id == id)
                    .FirstOrDefault();

                if (trip == null)
                {
                    return ServiceResult<TripDetailsVM>.FailureResult("Trip not found.");
                }

                var result = trip.ToDetails(peopleCount);

                return ServiceResult<TripDetailsVM>.SuccessResult(result, "Enrollment retrieved successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return ServiceResult<TripDetailsVM>.FailureResult(ex.Message);
            }
        }

    }
}
