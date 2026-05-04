

namespace JuniorPharon.Models
{
    public class Wishlist
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<WishlistItem> Items { get; set; }
    }
}