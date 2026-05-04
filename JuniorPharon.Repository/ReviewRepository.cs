using Infrastructure.SqlServer;
using JuniorPharon.Models;
using JuniorPharon.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorPharon.Repository
{
    public class ReviewRepository : BaseRepository<Review>
    {
        public ReviewRepository(DBContext context) : base(context)
        {
        }


        public async Task<List<Review>> GetRreviewsByTripIdAsync(int tripId)
        {
            try
            {
                // Fetch data from DB (EF part)
                //var enrollments =  GetList(e => e.TeacherId == teacherId && e.Status == EnrollmentStatus.Active)
                var reviews = GetList(e => e.TripId == tripId).ToList();



                // Map using your extension method (in-memory)
                //return await GetList(e => e.TeacherId == teacherId).Select(e => e.Student.ToList()).ToListAsync();
                //
                return reviews;
            }
            catch
            {
                throw;

            }
        }
    }
}
