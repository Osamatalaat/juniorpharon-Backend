using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorPharon.Models.TripsContents
{
    public class TripContentConfigurtion : IEntityTypeConfiguration<TripContent>
    {
        public void Configure(EntityTypeBuilder<TripContent> builder)
        {
            builder.HasKey(tc => tc.Id);
            builder.Property(tc => tc.Title).IsRequired().HasColumnType("NVARCHAR(MAX)");
            builder.Property(tc => tc.Description).IsRequired().HasColumnType("NVARCHAR(MAX)");
            builder.Property(tc => tc.LanguageCode).IsRequired();

            // Foreign key relationship with Trip
            builder.HasOne(tc => tc.Trip)
                   .WithMany(t => t.TripContents)
                   .HasForeignKey(tc => tc.TripId)
                   .OnDelete(DeleteBehavior.NoAction); // Optional: Define delete behavior
        }

    }
}
