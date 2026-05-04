using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuniorPharon.Models;

public class TripConfiguration : IEntityTypeConfiguration<Trip>
{
    public void Configure(EntityTypeBuilder<Trip> builder)
    {
        builder.HasKey(t => t.Id);
      
        builder.Property(t => t.DurationInDays).IsRequired();
        builder.Property(t => t.CreatedAt).HasDefaultValue(DateTime.Now);
        builder.Property(t => t.Location).IsRequired();
        //builder.Property(t => t.Price).IsRequired();

        
        //Relations : 

        //builder.HasMany(t => t.Bookings)
        //    .WithOne(t => t.Trip)
        //    .HasForeignKey(t => t.TripId)
        //    .OnDelete(DeleteBehavior.NoAction);

        //builder.HasMany(t => t.Reviews)
        //    .WithOne(t => t.Trip)
        //    .HasForeignKey(t => t.TripId)
        //    .OnDelete(DeleteBehavior.NoAction);


        builder.HasMany(t => t.TripImages)
            .WithOne(t => t.Trip)
            .HasForeignKey(t => t.TripId)
            .OnDelete(DeleteBehavior.NoAction);

      

    }
}