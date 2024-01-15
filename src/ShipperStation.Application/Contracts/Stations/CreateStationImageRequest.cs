namespace ShipperStation.Application.Contracts.Stations;
public sealed record CreateStationImageRequest
{
    public string ImageUrl { get; set; } = default!;
}
