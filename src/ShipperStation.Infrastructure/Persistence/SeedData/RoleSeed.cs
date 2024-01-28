using ShipperStation.Domain.Constants;
using ShipperStation.Domain.Entities.Identities;

namespace ShipperStation.Infrastructure.Persistence.SeedData;
internal static class RoleSeed
{
    public static IList<Role> Default => new List<Role>()
    {
        new(RoleName.Admin),
        new(RoleName.User),
        new(RoleName.Staff),
        new(RoleName.StationManager),
    };
}
