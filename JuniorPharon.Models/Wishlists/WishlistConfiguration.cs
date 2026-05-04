using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JuniorPharon.Models;

namespace JuniorPharon.Models
{
    public class WishlistConfiguration : IEntityTypeConfiguration<Wishlist>
    {
        public void Configure(EntityTypeBuilder<Wishlist> builder)
        {
            builder.HasKey(w => w.Id);

           

            // 🔗 Relations

            builder.HasOne(w => w.User)
                   .WithMany()
                   .HasForeignKey(w => w.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(w => w.Items)
                   .WithOne(i => i.Wishlist)
                   .HasForeignKey(i => i.WishlistId)
                   .OnDelete(DeleteBehavior.Cascade);

            // 🔥 كل User ليه Wishlist واحدة (اختياري)
            builder.HasIndex(w => w.UserId).IsUnique();
        }
    }
}