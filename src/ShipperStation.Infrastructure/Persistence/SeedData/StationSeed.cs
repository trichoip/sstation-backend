using ShipperStation.Domain.Entities;

namespace ShipperStation.Infrastructure.Persistence.SeedData;
internal static class StationSeed
{
    public static IList<Station> Default => new List<Station>()
    {
        new()
        {
            Name = "Station 1",
            Description = "Station 1",
            Address = "Station 1",
            ContactPhone = "0123456789",
            Latitude = "10.762622",
            Longitude = "106.660172",
        }
    };
}
