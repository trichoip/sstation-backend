using MediatR;
using ShipperStation.Application.Models;

namespace ShipperStation.Application.Features.Zones.Commands;
public sealed record DeleteZoneCommand(int Id) : IRequest<MessageResponse>
{
    public int StationId { get; set; }
}