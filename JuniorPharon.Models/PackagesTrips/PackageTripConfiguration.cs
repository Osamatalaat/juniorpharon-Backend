using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JuniorPharon.Models;

namespace JuniorPharon.Models
{
    public class PackageTripConfiguration : IEntityTypeConfiguration<PackageTrip>
    {
        public void Configure(EntityTypeBuilder<PackageTrip> builder)
        {
            // 🔥 Composite Key
            builder.HasKey(pt => new { pt.PackageId, pt.TripId });

            builder.Property(pt => pt.DayOrder)
                   .IsRequired();

            // 🔗 Relations

            builder.HasOne(pt => pt.Package)
                   .WithMany(p => p.PackageTrips)
                   .HasForeignKey(pt => pt.PackageId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pt => pt.Trip)
                   .WithMany() // أو .WithMany(t => t.PackageTrips) لو هتزودها
                   .HasForeignKey(pt => pt.TripId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}