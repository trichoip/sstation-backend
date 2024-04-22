using ShipperStation.Application.Models;

namespace ShipperStation.Application.Features.Stations.Models;
public sealed record StationResponseOfPayment : BaseAuditableEntityResponse<int>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string? ContactPhone { get; set; }
    public string Address { get; set; } = default!;
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }

    public double Balance { get; set; }

    public ICollection<StationImageResponse> StationImages { get; set; } = new HashSet<StationImageResponse>();
}
