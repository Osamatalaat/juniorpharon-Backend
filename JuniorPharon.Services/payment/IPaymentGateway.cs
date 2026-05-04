
using JuniorPharon.Models.Enums;
using JuniorPharon.ViewModels;

namespace JuniorPharon.Services
{
    public interface IPaymentGateway
    {
        PaymentMethod _PaymentMethod { get; }
        Task<PaymentResult> CreatePaymentAsync(PaymentCreateVM request);
    }

}