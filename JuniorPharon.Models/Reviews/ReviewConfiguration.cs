using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuniorPharon.Models;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(r => r.Id);

        // ⚡ Performance Indexes
        builder.HasIndex(r => r.TripId);
        builder.HasIndex(r => r.PackageId);
        builder.HasIndex(r => r.ClientId);

        // 🔥 Composite Index (مهم للـ queries)
        builder.HasIndex(r => new { r.TripId, r.CreationDate });
        builder.HasIndex(r => new { r.PackageId, r.CreationDate });

        // 🧱 Properties
        builder.Property(r => r.Comment)
               .IsRequired()
               .HasMaxLength(1000);

        builder.Property(r => r.Rating)
               .IsRequired();

        builder.Property(r => r.CreationDate)
               .HasDefaultValueSql("GETDATE()");

        // 🔗 Relations

        builder.HasOne(r => r.Trip)
               .WithMany(t => t.Reviews)
               .HasForeignKey(r => r.TripId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(r => r.Package)
               .WithMany(p => p.Reviews) // 🔥 انت ضايفها في Package ✔
               .HasForeignKey(r => r.PackageId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(r => r.Client)
               .WithMany(u => u.Reviews)
               .HasForeignKey(r => r.ClientId)
               .OnDelete(DeleteBehavior.Cascade);

        // ✅ Constraints (كلهم في ToTable واحدة)
        builder.ToTable(t =>
        {
            // واحد بس Trip أو Package
            t.HasCheckConstraint(
                "CK_Review_TripOrPackage",
                "(TripId IS NOT NULL AND PackageId IS NULL) OR (TripId IS NULL AND PackageId IS NOT NULL)"
            );

            // Rating من 1 لـ 5
            t.HasCheckConstraint(
                "CK_Review_Rating",
                "Rating >= 1 AND Rating <= 5"
            );
        });
    }
}