using Infrastructure.SqlServer;
using JuniorPharon.Models;
using JuniorPharon.ViewModels;


namespace JuniorPharon.Repository
{
    public class PaymentRepository : BaseRepository<Payment>
    {
        public PaymentRepository(DBContext context) : base(context)
        {
        }

        public async Task<List<Payment>> GetPaymentByClientIdAsync(string clientId)
        {
            try
            {
                // Fetch data from DB (EF part)
                //var payments = GetList(e => e.ClientId == clientId).Select(p => p.ToDetails()).ToList();

                if (string.IsNullOrEmpty(clientId))
                {
                    throw new ArgumentException("Client ID cannot be null or empty.", nameof(clientId));
                }

                var payments =  GetList(e => e.ClientId == clientId).ToList();

                // Map using your extension method (in-memory)

                return payments;
            }
            catch
            {
                throw;
            }
        }
    }

}
