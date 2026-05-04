using JuniorPharon.Models;
using JuniorPharon.Repository;
using JuniorPharon.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace JuniorPharon.Services
{
    public class ReviewService
    {

        private readonly UnitOfWork _unitOfWork;
        private readonly ILogger<ReviewService> _logger;

        public ReviewService(UnitOfWork unitOfWork , ILogger<ReviewService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ServiceResult> CreateReview(ReviewCreateVM review)
        {
            try
            {
                if (review == null)
                    return ServiceResult.FailureResult("review is empty.");
                await _unitOfWork._reviewRepository.AddAsync(review.ToCreate());
                await _unitOfWork.SaveChangesAsync();
                return ServiceResult.SuccessResult("review created successfully.", System.Net.HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return ServiceResult.FailureResult($"An error occurred while creating the review: {ex.Message}", System.Net.HttpStatusCode.InternalServerError);
            }
        }


        public async Task<ServiceResult<List<ReviewDetailsVM>>> GetReviewsByTripId(int tripId)
        {
            try
            {
                var reviews = await _unitOfWork._reviewRepository.GetRreviewsByTripIdAsync(tripId);
                if (reviews == null)
                    return ServiceResult<List<ReviewDetailsVM>>.FailureResult("reviews not found.");

                var reviewDetails = reviews.Select(r => r.ToDetails()).ToList();
                return ServiceResult<List<ReviewDetailsVM>>.SuccessResult(reviewDetails, "Reviews retrieved successfully.", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ServiceResult<List<ReviewDetailsVM>>.FailureResult($"An error occurred while retrieving reviews: {ex.Message}", System.Net.HttpStatusCode.InternalServerError);
            }
        }


        //public async Task<ServiceResult<List<ReviewDetailsVM>>> GetReviewsByUserId(string userId)
        //{
        //    try
        //    {
        //        var reviews = await _unitOfWork._reviewRepository.GetList(r => r.UserId == userId).ToListAsync();
        //        if (reviews == null)
        //            return ServiceResult<List<ReviewDetailsVM>>.FailureResult("reviews not found.");
        //        var reviewDetails = reviews.Select(r => r.ToDetails()).ToList();
        //        return ServiceResult<List<ReviewDetailsVM>>.SuccessResult(reviewDetails, "Reviews retrieved successfully.", System.Net.HttpStatusCode.OK);
        //    }
        //    catch (Exception ex)
        //    {
        //        return ServiceResult<List<ReviewDetailsVM>>.FailureResult($"An error occurred while retrieving reviews: {ex.Message}", System.Net.HttpStatusCode.InternalServerError);
        //    }




        //}


        public async Task<ServiceResult> DeleteReview(int reviewId)
        {
            try
            {
                var review = await _unitOfWork._reviewRepository.GetByIdAsync(reviewId);
                if (review == null)
                    return ServiceResult.FailureResult("Review not found.");
                _unitOfWork._reviewRepository.Delete(review);
                await _unitOfWork.SaveChangesAsync();
                return ServiceResult.SuccessResult("Review deleted successfully.", System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ServiceResult.FailureResult($"An error occurred while deleting the review: {ex.Message}", System.Net.HttpStatusCode.InternalServerError);
            }



        }



        public async Task<ServiceResult> UpdateReview(int reviewId, ReviewEditVM reviewUpdate)
        {
            try
            {
                if (reviewUpdate == null)
                    return ServiceResult.FailureResult("Review update data is empty.");

                var review = await _unitOfWork._reviewRepository.GetByIdAsync(reviewId);

                if (review == null)
                    return ServiceResult.FailureResult("Review not found.");

                // تحديث البيانات
                review.UpdateFromEditVM(reviewUpdate);

                await _unitOfWork.SaveChangesAsync();

                return ServiceResult.SuccessResult("Review updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating review {ReviewId}", reviewId);

                return ServiceResult.FailureResult(
                    "An error occurred while updating the review.",
                    HttpStatusCode.InternalServerError);
            }
        }

    }
}
