

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuniorPharon.Models
{
   public class WishlistItemConfiguration : IEntityTypeConfiguration<WishlistItem>
{
    public void Configure(EntityTypeBuilder<WishlistItem> builder)
    {
        builder.HasKey(w => w.Id);

        builder.Property(w => w.CreatedAt)
               .HasDefaultValueSql("GETDATE()");

        // 🔗 Relations

        builder.HasOne(w => w.Wishlist)
               .WithMany(w => w.Items)
               .HasForeignKey(w => w.WishlistId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(w => w.Trip)
               .WithMany()
               .HasForeignKey(w => w.TripId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(w => w.Package)
               .WithMany()
               .HasForeignKey(w => w.PackageId)
               .OnDelete(DeleteBehavior.Restrict);

            // 🔥 CHECK CONSTRAINT
            builder.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CK_WishlistItem_TripOrPackage",
                    "(TripId IS NOT NULL AND PackageId IS NULL) OR (TripId IS NULL AND PackageId IS NOT NULL)"
                );
            });

            // 🔥 منع التكرار
            builder.HasIndex(w => new { w.WishlistId, w.TripId }).IsUnique();
        builder.HasIndex(w => new { w.WishlistId, w.PackageId }).IsUnique();

        // 🔥 Performance
        builder.HasIndex(w => w.TripId);
        builder.HasIndex(w => w.PackageId);
    }
}
}
