using Infrastructure.SqlServer;
using JuniorPharon.Models;
using JuniorPharon.Models.Enums;
using JuniorPharon.ViewModels;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorPharon.Repository
{
    public class BookingRepository : BaseRepository<Booking>
    {
        public BookingRepository(DBContext context) : base(context)
        {
        }


        public async Task<PaginationVM<BookingDetailsVM>> GetAllBookingAsync(
            int pageSize = 10,
            int pageIndex = 1
            )
        {
            try
            {
                return await SearchAsync(
                    null,                          // no filter (يعني هات الكل)
                    m => m.BookDate,              // order by StartDate
                    m => m.ToDetails(),            // projection
                    false,                         // ascending
                    pageSize,
                    pageIndex
                );
            }
            catch
            {
                throw;
            }
        }



        public async Task<PaginationVM<BookingDetailsVM>> SearchBookingDetails(
      string? userId = null,
      DateTime? bookingDate = null,
      DateTime? startDate = null,
      BookingStatus? status = null,
      bool descending = false,
      int pageSize = 10,
      int pageIndex = 1)
        {
            try
            {
                var predicate = PredicateBuilder.New<Booking>(true);

                if (!string.IsNullOrWhiteSpace(userId))
                    predicate = predicate.And(b => b.ClientId == userId);

                if (bookingDate.HasValue)
                    predicate = predicate.And(b =>
                        b.BookDate.Date == bookingDate.Value.Date);

                if (startDate.HasValue)
                    predicate = predicate.And(b =>
                        b.StartDate.Date >= startDate.Value.Date);

                if (status.HasValue)
                    predicate = predicate.And(b => b.Status == status.Value);

                return await SearchAsync(
                    predicate,
                    orderBy: b => b.BookDate,   // أو BookingDate
                    selector: b => b.ToDetails(),
                    descending: descending,
                    pageSize: pageSize,
                    pageIndex: pageIndex
                );
            }
            catch
            {
                throw;
            }
        }


        public async Task<PaginationVM<BookingDetailsVM>> SearchBookingsByClientId(
         string clientId,
         int pageSize = 10,
         int pageIndex = 1)
        {
            try
            {
                return await SearchAsync(
                    m => m.ClientId == clientId,
                    m => m.BookDate,
                    m => m.ToDetails(),
                    false,
                    pageSize,
                    pageIndex
                );
            }
            catch
            {
                throw;
            }
        }


        public async Task<PaginationVM<BookingDetailsVM>> GetBookingsByStatusAsync(
            BookingStatus status,
            int pageSize = 10,
            int pageIndex = 1)
        {
            try
            {
                return await SearchAsync(
                    m => m.Status == status,         // Filter by status
                    m => m.BookDate,                // Order by StartDate
                    m => m.ToDetails(),              // Project to Details VM
                    false,                           // Ascending order
                    pageSize,
                    pageIndex
                );
            }
            catch
            {
                throw;
            }
        }



    }
}
