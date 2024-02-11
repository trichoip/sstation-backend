using MediatR;
using ShipperStation.Application.Features.Zones.Models;

namespace ShipperStation.Application.Features.Zones.Queries;
public sealed record GetZoneByIdQuery(int Id) : IRequest<ZoneResponse>
{
    public int StationId { get; set; }
}