using Infrastructure.SqlServer;
using JuniorPharon.Models;


namespace JuniorPharon.Repository
{
    public class NotificationRepository : BaseRepository<Notification>
    {
        public NotificationRepository(DBContext context) : base(context)
        {
        }
    }
}
