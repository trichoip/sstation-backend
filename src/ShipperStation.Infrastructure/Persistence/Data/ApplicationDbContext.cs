using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShipperStation.Domain.Entities;
using System.Reflection;

namespace ShipperStation.Infrastructure.Persistence.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options)
    {
        private const string Prefix = "AspNet";

        public DbSet<Station> Stations { get; set; }
        public DbSet<StationImage> StationImages { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<DataImage> DataImages { get; set; }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<PackageImage> PackageImages { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderHistory> OrderHistories { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Rack> Racks { get; set; }
        public DbSet<Slot> Slots { get; set; }
        public DbSet<Shelf> Shelves { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Audit> Audits { get; set; }

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
        }
    }
}
