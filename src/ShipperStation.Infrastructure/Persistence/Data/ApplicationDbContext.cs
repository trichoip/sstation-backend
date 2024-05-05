using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Entities.Identities;
using System.Reflection;

namespace ShipperStation.Infrastructure.Persistence.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>(options)
{
    private const string Prefix = "AspNet";

    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Device> Devices { get; set; }
    public DbSet<Package> Packages { get; set; }
    public DbSet<PackageImage> PackageImages { get; set; }
    public DbSet<Rack> Racks { get; set; }
    public DbSet<Shelf> Shelves { get; set; }
    public DbSet<Station> Stations { get; set; }
    public DbSet<StationImage> StationImages { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<UserStation> UserStations { get; set; }
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<PackageStatusHistory> PackageStatusHistories { get; set; }
    public DbSet<Pricing> Pricings { get; set; }
    public DbSet<Zone> Zones { get; set; }
    public DbSet<DefaultPricing> DefaultPricings { get; set; }
    public DbSet<Payment> Payments { get; set; }

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

        modelBuilder.Entity<UserRole>(b =>
        {
            b.HasOne(e => e.Role)
                .WithMany(e => e.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            b.HasOne(e => e.User)
                .WithMany(e => e.UserRoles)
                .HasForeignKey(ur => ur.UserId);
        });
    }
}
