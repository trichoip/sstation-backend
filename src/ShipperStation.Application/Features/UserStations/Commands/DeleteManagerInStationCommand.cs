using MediatR;
using ShipperStation.Application.Models;

namespace ShipperStation.Application.Features.UserStations.Commands;
public sealed record DeleteManagerInStationCommand(Guid Id) : IRequest<MessageResponse>
{
    public int StationId { get; set; }
}
