using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShipperStation.Domain.Entities.Identities;

namespace ShipperStation.Infrastructure.Persistence.Configurations;
internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(c => c.FullName).HasMaxLength(50);
    }
}