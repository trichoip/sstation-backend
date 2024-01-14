using ShipperStation.Domain.Constants;
using ShipperStation.Domain.Entities.Identities;

namespace ShipperStation.Infrastructure.Persistence.SeedData;
internal static class RoleSeed
{
    public static IList<Role> Default => new List<Role>()
    {
        new(Roles.Admin),
        new(Roles.User),
        new(Roles.Staff),
        new(Roles.StationManager),
    };
}
