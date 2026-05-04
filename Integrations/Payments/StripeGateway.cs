using JuniorPharon.Services;
using JuniorPharon.ViewModels;
using JuniorPharon.Models.Enums;
using Stripe.Checkout;

namespace Integrations.Payments
{
    public class StripeGateway : IPaymentGateway
    {
        public PaymentMethod _PaymentMethod => PaymentMethod.Visa; // مثال

        public async Task<PaymentResult> CreatePaymentAsync(PaymentCreateVM request)
        {
            try
            {
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>
                    {
                        new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                UnitAmount = (long)(request.Amount * 100), // بالـ cents
                                Currency = request.Currency.ToLower(),
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = $"Booking {request.BookingId}"
                                }
                            },
                            Quantity = 1
                        }
                    },
                    Mode = "payment",
                    SuccessUrl = "https://yourdomain.com/payment-success?session_id={CHECKOUT_SESSION_ID}",
                    CancelUrl = "https://yourdomain.com/payment-cancel"
                };

                var service = new SessionService();
                Session session = await service.CreateAsync(options);

                return new PaymentResult
                {
                    Success = true,
                    TransactionId = session.Id,
                    RedirectUrl = session.Url
                };
            }
            catch (Exception ex)
            {
                return new PaymentResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}