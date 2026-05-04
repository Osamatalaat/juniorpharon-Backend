using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuniorPharon.Models;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.HasKey(n => n.Id);

        builder.Property(n => n.Description)
               .IsRequired()
               .HasMaxLength(500);

        builder.Property(n => n.Type)
               .IsRequired()
               .HasConversion<string>(); // 🔥 أفضل من int

        builder.Property(n => n.IsRead)
               .HasDefaultValue(false);

        builder.Property(n => n.CreatedAt)
               .HasDefaultValueSql("GETDATE()");

        builder.Property(n => n.SenderId)
               .IsRequired();

        builder.Property(n => n.ReceiverId)
               .IsRequired();

        // 🔥🔥 أهم جزء (حل المشكلة)

        // 🟢 Sender
        builder.HasOne(n => n.Sender)
               .WithMany(u => u.SentNotifications)
               .HasForeignKey(n => n.SenderId)
               .OnDelete(DeleteBehavior.NoAction);

        // 🔵 Receiver
        builder.HasOne(n => n.Receiver)
               .WithMany(u => u.ReceivedNotifications)
               .HasForeignKey(n => n.ReceiverId)
               .OnDelete(DeleteBehavior.NoAction);

        // ⚡ Performance
        builder.HasIndex(n => n.SenderId);
        builder.HasIndex(n => n.ReceiverId);
    }
}