using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Enums;

namespace ShipperStation.Infrastructure.Persistence.SeedData;

public static class UserSeeding
{
    public static IList<User> DefaultAccounts => new List<User>()
    {
        new()
        {
            UserName = "admin",
            PhoneNumber = "0000000",
            Status = UserStatus.Active,
            PasswordHash = "asdsad"
        }
    };
}