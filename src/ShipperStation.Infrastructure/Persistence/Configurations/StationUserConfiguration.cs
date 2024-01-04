using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Infrastructure.Persistence.Configurations;
internal class StationUserConfiguration : IEntityTypeConfiguration<UserStation>
{
    public void Configure(EntityTypeBuilder<UserStation> builder)
    {
        builder.HasKey(c => new { c.StationId, c.UserId });
    }
}
