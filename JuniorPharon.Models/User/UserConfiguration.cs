using JuniorPharon.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace JuniorPharon.Models;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {

        //builder.Property(b => b.Role).HasDefaultValue(Roles.Client);
        builder.Property(b => b.ModificationDate).HasDefaultValue(DateTime.Now);
        builder.Property(b => b.CreationDate).HasDefaultValue(DateTime.Now);

        builder.HasKey(x => x.Id);

        builder.HasOne(u => u.Admin)
            .WithOne(a => a.User)
            .HasForeignKey<Admin>(a => a.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(u => u.Client)
           .WithOne(a => a.User)
           .HasForeignKey<Client>(a => a.UserId)
           .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(b => b.Bookings)
            .WithOne(u => u.Client)
            .HasForeignKey(b => b.ClientId);
        
        builder.HasMany(b => b.Reviews)
            .WithOne(r => r.Client)
            .HasForeignKey(r => r.ClientId);
        
        //builder.HasMany(b => b.SentMessages)
        //    .WithOne(m => m.Sender)
        //    .HasForeignKey(m => m.SenderId)
        //    .OnDelete(DeleteBehavior.NoAction); // 🔴 IMPORTANT


        //builder.HasMany(b => b.ReceivedMessages)
        //    .WithOne(m => m.Receiver)
        //    .HasForeignKey(m => m.ReceiverId)
        //    .OnDelete(DeleteBehavior.NoAction); // 🔴 IMPORTANT


     //   builder.HasMany(u => u.SentNotifications)
     //.WithOne(n => n.Sender)
     //.HasForeignKey(n => n.SenderId)
     //.OnDelete(DeleteBehavior.NoAction);

     //   builder.HasMany(u => u.ReceivedNotifications)
     //       .WithOne(n => n.Receiver)
     //       .HasForeignKey(n => n.ReceiverId)
     //       .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(b => b.Trips)
            .WithOne(t => t.CreatedByUser)
            .HasForeignKey(t => t.CreatedBy);
        
    }
}