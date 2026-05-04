using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuniorPharon.Models;

public class TripImageConfiguration : IEntityTypeConfiguration<TripImage>
{
    public void Configure(EntityTypeBuilder<TripImage> builder)
    {
        builder.HasKey(img => img.Id);
        builder.Property(img => img.ImageUrl).IsRequired();
        builder.Property(img => img.IsCover).HasDefaultValue(false);

        builder.HasIndex(t => new { t.TripId, t.IsCover })
       .HasFilter("[IsCover] = 1")
       .IsUnique();
    }
}