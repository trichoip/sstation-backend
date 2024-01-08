using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Entities.Identities;
using System.Reflection;

namespace ShipperStation.Infrastructure.Persistence.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>(options)
{
    private const string Prefix = "AspNet";

    public DbSet<Station> Stations { get; set; }
    public DbSet<StationImage> StationImages { get; set; }
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<Zone> Zones { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Package> Packages { get; set; }
    public DbSet<PackageImage> PackageImages { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderHistory> OrderHistories { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Rack> Racks { get; set; }
    public DbSet<Slot> Slots { get; set; }
    public DbSet<Shelf> Shelves { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<UserStation> UserStations { get; set; }
    public DbSet<Device> Devices { get; set; }
    public DbSet<Size> Sizes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();
            if (tableName != null && tableName.StartsWith(Prefix))
            {
                entityType.SetTableName(tableName.Substring(6));
            }
        }

        modelBuilder.Entity<User>(b =>
        {
            b.HasMany(e => e.UserRoles)
                .WithOne()
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        });

        modelBuilder.Entity<UserRole>(b =>
        {
            b.HasOne(e => e.Role)
                .WithMany()
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
        });
    }
}
