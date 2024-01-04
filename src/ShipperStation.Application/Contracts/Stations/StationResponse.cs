using ShipperStation.Application.Contracts.Zones;

namespace ShipperStation.Application.Contracts.Stations;
public sealed record StationResponse : BaseAuditableEntityResponse<int>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string? ContactPhone { get; set; }
    public string Address { get; set; } = default!;
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }

    public ICollection<StationImageResponse> StationImages { get; set; } = new HashSet<StationImageResponse>();
    public ICollection<ZoneResponse> Zones { get; set; } = new HashSet<ZoneResponse>();
}
