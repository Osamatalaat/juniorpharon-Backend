

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuniorPharon.Models
{
    public class PricingTierConfiguration : IEntityTypeConfiguration<PricingTier>
    {
        public void Configure(EntityTypeBuilder<PricingTier> builder)
        {
            builder.HasKey(p => p.Id);

            // 🔥 Indexes (مهم جدًا)
            builder.HasIndex(p => new { p.TripId, p.MinPeople, p.MaxPeople });
            builder.HasIndex(p => new { p.PackageId, p.MinPeople, p.MaxPeople });

            // Optional تحسين
            builder.HasIndex(p => p.TripId).HasFilter("[TripId] IS NOT NULL");
            builder.HasIndex(p => p.PackageId).HasFilter("[PackageId] IS NOT NULL");

            builder.Property(p => p.PricePerPerson)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            // Relations
            builder.HasOne(p => p.Trip)
                   .WithMany(t => t.PricingTiers)
                   .HasForeignKey(p => p.TripId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Package)
                   .WithMany(p => p.PricingTiers)
                   .HasForeignKey(p => p.PackageId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Constraints
            builder.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CK_PricingTier_MinMax",
                    "MinPeople <= MaxPeople"
                );

                t.HasCheckConstraint(
                    "CK_PricingTier_TripOrPackage",
                    "(TripId IS NOT NULL AND PackageId IS NULL) OR (TripId IS NULL AND PackageId IS NOT NULL)"
                );
            });
        }
    }
}
