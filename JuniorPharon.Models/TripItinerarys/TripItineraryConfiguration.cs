using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuniorPharon.Models
{
    public class TripItineraryConfiguration : IEntityTypeConfiguration<TripItinerary>
    {
        public void Configure(EntityTypeBuilder<TripItinerary> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.DayNumber)
                   .IsRequired();

            builder.Property(i => i.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(i => i.Description)
                   .IsRequired();

            // 🔗 Relation
            builder.HasOne(i => i.Trip)
                   .WithMany(t => t.Itineraries)
                   .HasForeignKey(i => i.TripId)
                   .OnDelete(DeleteBehavior.Cascade);

            // 🔥 مهم جداً: منع تكرار نفس اليوم
            builder.HasIndex(i => new { i.TripId, i.DayNumber })
                   .IsUnique();

            // 🔥 Performance
            builder.HasIndex(i => i.TripId);
        }
    }
}