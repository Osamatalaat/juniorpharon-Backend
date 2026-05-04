using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuniorPharon.Models;


public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.HasKey(b => b.Id);

        // ⚡ Performance Indexes
        builder.HasIndex(b => b.TripId);
        builder.HasIndex(b => b.PackageId);
        builder.HasIndex(b => b.ClientId);

        builder.Property(b => b.BookDate)
               .HasColumnType("date")
               .HasDefaultValueSql("GETDATE()");

        builder.Property(b => b.TotalPrice)
               .HasColumnType("decimal(18,2)")
               .IsRequired();

        builder.Property(b => b.Status)
               .HasConversion<int>() // يخزن enum كـ int
               .HasDefaultValue(0);

        builder.Property(b => b.NumberOfPeople)
               .IsRequired();

        // 🔗 Relations

        builder.HasOne(b => b.Client)
               .WithMany(u => u.Bookings)
               .HasForeignKey(b => b.ClientId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(b => b.Trip)
               .WithMany(t => t.Bookings)
               .HasForeignKey(b => b.TripId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(b => b.Package)
               .WithMany()
               .HasForeignKey(b => b.PackageId)
               .OnDelete(DeleteBehavior.NoAction);

        // 🔥 CHECK CONSTRAINT (مهم جداً)
        builder.ToTable(t =>
        {
            t.HasCheckConstraint(
                "CK_Booking_TripOrPackage",
                "(TripId IS NOT NULL AND PackageId IS NULL) OR (TripId IS NULL AND PackageId IS NOT NULL)"
            );
        });
    }
}