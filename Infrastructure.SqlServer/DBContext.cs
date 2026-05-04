using JuniorPharon.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.SqlServer;

public class DBContext : IdentityDbContext<User>
{
    public DBContext(DbContextOptions<DBContext> options) : base(options)
    {
    }

    // 🧱 DbSets
    public DbSet<Trip> Trips { get; set; }
    public DbSet<TripImage> TripImages { get; set; }
    public DbSet<TripContent> TripContents { get; set; }
    public DbSet<TripItinerary> TripItineraries { get; set; }

    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Payment> Payments { get; set; }

    public DbSet<Review> Reviews { get; set; }

    public DbSet<Package> Packages { get; set; }
    public DbSet<PackageTrip> PackageTrips { get; set; }
    public DbSet<PackageContent> PackageContents { get; set; }

    public DbSet<PricingTier> PricingTiers { get; set; }
    public DbSet<DiscountCode> DiscountCodes { get; set; }

    public DbSet<Wishlist> Wishlists { get; set; }
    public DbSet<WishlistItem> WishlistItems { get; set; }

    public DbSet<Message> Messages { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Chat> Chats { get; set; }

    public DbSet<Admin> Admins { get; set; }
    public DbSet<Client> Clients { get; set; }

    // ⚙️ Configurations
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // 🔥 يطبق كل الـ Configurations تلقائي
        builder.ApplyConfigurationsFromAssembly(typeof(DBContext).Assembly); // Infrastructure

        builder.ApplyConfigurationsFromAssembly(typeof(Trip).Assembly); // 🔥 Models
        //builder.ApplyConfiguration(new MessageConfiguration());
        //builder.ApplyConfiguration(new NotificationConfiguration());
        //builder.ApplyConfiguration(new ChatConfiguration());
        //builder.ApplyConfiguration(new AdminConfiguration());
        //builder.ApplyConfiguration(new ClientConfiguration());
        //builder.ApplyConfiguration(new TripConfiguration());
        //builder.ApplyConfiguration(new UserConfiguration());
        //builder.ApplyConfiguration(new BookingConfiguration());



        // 💡 Optional: Naming Convention
        foreach (var entity in builder.Model.GetEntityTypes())
        {
            entity.SetTableName(entity.GetTableName());
        }
    }
}