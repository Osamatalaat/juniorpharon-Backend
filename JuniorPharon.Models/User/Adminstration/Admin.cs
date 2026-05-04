using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorPharon.Models
{
    public class Admin
    {
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
