using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuniorPharon.Models;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Method)
            .IsRequired()
            .HasConversion<string>(); // Store as string (e.g. "Cash", "Visa")

        builder.Property(p => p.IsDone)
            .HasDefaultValue(false);

        builder.Property(p => p.Amount)
       .HasColumnType("decimal(18,2)");

        builder.Property(p => p.BookingId)
            .IsRequired();

        builder.HasOne(p => p.Booking)
            .WithOne(b => b.Payment)
            .HasForeignKey<Payment>(p => p.BookingId);

        builder.HasOne(p => p.Client)
            .WithMany(c => c.Payments)
            .HasForeignKey(p => p.ClientId)
            .OnDelete(DeleteBehavior.NoAction); // prevent cascade delete
    }
}