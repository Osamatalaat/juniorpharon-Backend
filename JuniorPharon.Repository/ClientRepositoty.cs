using Infrastructure.SqlServer;
using JuniorPharon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorPharon.Repository
{
    public class ClientRepositoty : BaseRepository<Client>
    {
        public ClientRepositoty(DBContext context) : base(context)
        {

        }
    }
}
