

using JuniorPharon.Models;

namespace JuniorPharon.ViewModels
{
    public static class PaymentExt
    {
        public static Payment ToCreate(this PaymentCreateVM vm)
        {
            return new Payment
            {
                Method = vm.Method,
                BookingId = vm.BookingId,
                ClientId = vm.ClientId,
                Amount = vm.Amount,
                Currency = vm.Currency ?? "EGP", // Default to EGP if not provided
                TransactionId = vm.TransactionId,
            };
        }
        public static PaymentDetailsVM ToDetails(this Payment payment)
        {
            return new PaymentDetailsVM
            {
                Id = payment.Id,
                Method = payment.Method,
                Amount = payment.Amount,
                Currency = payment.Currency,
                IsDone = payment.IsDone,
                BookingId = payment.BookingId,
                ClientId = payment.ClientId,
                ClientName = payment.Client?.FirstName + " " + payment.Client?.LastName,
                ClientImg = payment.Client?.ProfileImg,
            };
        }
    }
}
