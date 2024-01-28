namespace ShipperStation.Application.Features.Stations.Models;
public sealed record CreateStationImageRequest
{
    public string ImageUrl { get; set; } = default!;
}
