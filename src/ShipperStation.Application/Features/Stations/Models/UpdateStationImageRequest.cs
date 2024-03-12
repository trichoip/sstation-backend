namespace ShipperStation.Application.Features.Stations.Models;
public sealed record UpdateStationImageRequest
{
    public int Id { get; set; }
    public string ImageUrl { get; set; } = default!;
}
