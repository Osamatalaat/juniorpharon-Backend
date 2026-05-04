
using Infrastructure.SqlServer;
using JuniorPharon.Models;

namespace JuniorPharon.Repository
{
    public class AdminRepository : BaseRepository<Admin>
    {
        public AdminRepository(DBContext context) : base(context)
        {
        }
    }
}
