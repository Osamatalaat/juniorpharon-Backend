using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorPharon.Models
{
    public class WishlistItem
    {
        public int Id { get; set; }

        public int WishlistId { get; set; }

        public int? TripId { get; set; }
        public int? PackageId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual Wishlist Wishlist { get; set; }
        public virtual Trip Trip { get; set; }
        public virtual Package Package { get; set; }
    }
}
