using JuniorPharon.Repository;
using JuniorPharon.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace JuniorPharon.Services
{
    public class PaymentService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IPaymentGateway _IpaymentGateway;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(UnitOfWork unitOfWork,IPaymentGateway paymentGateway, ILogger<PaymentService> logger)
        {
            _unitOfWork = unitOfWork;
            _IpaymentGateway = paymentGateway;
            _logger = logger;
        }

        public async Task<ServiceResult<PaymentInitResponse>> CreatePaymentAsync(PaymentCreateVM vm)
        {
            try
            {
                var booking = await _unitOfWork._bookingRepository.GetByIdAsync(vm.BookingId);

                if (booking == null)
                {
                    _logger.LogWarning("Payment failed: Booking not found. BookingId: {BookingId}", vm.BookingId);

                    return ServiceResult<PaymentInitResponse>
                           .FailureResult("Booking not found.");
                }

                if (vm.Amount != booking.TotalPrice)
                {
                    _logger.LogWarning(
                        "Payment amount mismatch. BookingId: {BookingId}, Expected: {Expected}, Received: {Received}",
                        vm.BookingId,
                        booking.TotalPrice,
                        vm.Amount
                    );

                    return ServiceResult<PaymentInitResponse>
                           .FailureResult("Invalid payment amount.");
                }

                var gatewayResult = await _IpaymentGateway.CreatePaymentAsync(vm);

                if (!gatewayResult.Success)
                {
                    _logger.LogError(
                        "Payment gateway failed. BookingId: {BookingId}, Message: {Message}",
                        vm.BookingId,
                        gatewayResult.Message
                    );

                    return ServiceResult<PaymentInitResponse>
                           .FailureResult(gatewayResult.Message);
                }

                var payment = vm.ToCreate();
                payment.IsDone = false;

                if(vm.ClientId  == null)
                {
                    _logger.LogWarning(
                        "Payment creation failed: ClientId is null. BookingId: {BookingId}",
                        vm.BookingId
                    );
                    return ServiceResult<PaymentInitResponse>
                           .FailureResult("Client information is missing.");
                }
                payment.ClientId = vm.ClientId;
                payment.TransactionId = gatewayResult.TransactionId;

                await _unitOfWork._paymentRepository.AddAsync(payment);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation(
                    "Payment initiated successfully. BookingId: {BookingId}, TransactionId: {TransactionId}",
                    vm.BookingId,
                    gatewayResult.TransactionId
                );

                return ServiceResult<PaymentInitResponse>.SuccessResult(
                    new PaymentInitResponse
                    {
                        RedirectUrl = gatewayResult.RedirectUrl
                    },
                    "Payment initiated successfully"
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Unexpected error while creating payment. BookingId: {BookingId}, ClientId: {ClientId}",
                    vm?.BookingId,
                    vm?.ClientId
                );

                return ServiceResult<PaymentInitResponse>
                       .FailureResult(
                           "An internal error occurred while processing the payment.",
                           System.Net.HttpStatusCode.InternalServerError
                       );
            }
        }
    }
}
