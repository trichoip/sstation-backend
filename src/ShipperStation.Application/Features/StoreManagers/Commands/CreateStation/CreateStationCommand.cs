using MediatR;
using ShipperStation.Application.Contracts;
using ShipperStation.Application.Contracts.Stations;

namespace ShipperStation.Application.Features.StoreManagers.Commands.CreateStation;
public sealed record CreateStationCommand : IRequest<MessageResponse>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string? ContactPhone { get; set; }
    public string Address { get; set; } = default!;
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }

    public IList<CreateStationImageRequest> StationImages { get; set; } = Array.Empty<CreateStationImageRequest>();
}
