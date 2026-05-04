

using Infrastructure.SqlServer;
using Microsoft.EntityFrameworkCore.Storage;

namespace JuniorPharon.Repository
{
    public class UnitOfWork : IDisposable
    {
        public AdminRepository _adminRepository { get; private set; }
        public ClientRepositoty _clientRepository { get; private set; }
        public UserRepository _userRepository { get; private set; }
        public BookingRepository _bookingRepository { get; private set; }

        public NotificationRepository _notificationRepository { get; private set; }

        public PaymentRepository _paymentRepository { get; private set; }
        public ReviewRepository _reviewRepository { get; private set; }
        public TripRepository _tripRepository { get; private set; }


        private readonly DBContext _context;

        public UnitOfWork(DBContext context,
            AdminRepository adminRepository,
            BookingRepository bookingRepository,
            NotificationRepository notificationRepository,
            PaymentRepository paymentRepository,
            ReviewRepository reviewRepository,
            TripRepository tripRepository,
            UserRepository userRepository
            )
        {
            _context = context;
            _adminRepository = adminRepository;
            _bookingRepository = bookingRepository;
            _notificationRepository = notificationRepository;
            _paymentRepository = paymentRepository;
            _reviewRepository = reviewRepository;
            _tripRepository = tripRepository;
            _userRepository = userRepository;

        }

        // ✅ Transaction method
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("An error occurred while saving changes to the database.", ex);
            }
        }
    }
}
