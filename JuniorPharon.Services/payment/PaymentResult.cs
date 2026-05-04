
namespace JuniorPharon.Services
{
    public class PaymentResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public string? TransactionId { get; set; }

        public string? RedirectUrl { get; set; }

        public string? ErrorCode { get; set; }
    }
}