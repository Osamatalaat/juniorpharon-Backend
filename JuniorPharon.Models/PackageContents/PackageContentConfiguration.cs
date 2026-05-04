using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuniorPharon.Models
{
    public class PackageContentConfiguration : IEntityTypeConfiguration<PackageContent>
    {
        public void Configure(EntityTypeBuilder<PackageContent> builder)
        {
            builder.HasKey(pc => pc.Id);

            builder.Property(pc => pc.Name)
                   .IsRequired()
                   .HasColumnType("nvarchar(200)");

            builder.Property(pc => pc.Description)
                   .IsRequired()
                   .HasColumnType("nvarchar(max)");

            builder.Property(pc => pc.LanguageCode)
                   .IsRequired();

            builder.HasOne(pc => pc.Package)
                   .WithMany(p => p.PackageContents)
                   .HasForeignKey(pc => pc.PackageId)
                   .OnDelete(DeleteBehavior.Cascade);

            // 🔥 مهم للـ performance
            builder.HasIndex(pc => new { pc.PackageId, pc.LanguageCode })
                   .IsUnique();
        }
    }
}