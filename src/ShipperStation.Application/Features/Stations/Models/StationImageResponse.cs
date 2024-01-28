using ShipperStation.Application.Models;

namespace ShipperStation.Application.Features.Stations.Models;
public sealed record StationImageResponse : BaseEntityResponse<int>
{
    public string ImageUrl { get; set; } = default!;
}
