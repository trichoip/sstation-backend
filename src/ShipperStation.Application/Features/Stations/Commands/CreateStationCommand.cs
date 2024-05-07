using MediatR;
using ShipperStation.Application.Features.Stations.Models;
using ShipperStation.Application.Models;

namespace ShipperStation.Application.Features.Stations.Commands;
public sealed record CreateStationCommand : IRequest<MessageResponse>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string? ContactPhone { get; set; }
    public string Address { get; set; } = default!;
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }

    public Guid ManagerId { get; set; }

    public IList<CreateStationImageRequest> StationImages { get; set; } = Array.Empty<CreateStationImageRequest>();
}
