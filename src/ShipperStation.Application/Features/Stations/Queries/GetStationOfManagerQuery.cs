using MediatR;
using ShipperStation.Application.Features.Stations.Models;

namespace ShipperStation.Application.Features.Stations.Queries;
public sealed record GetStationOfManagerQuery(int StationId) : IRequest<StationResponse>
{
    public Guid ManagerId { get; set; }
}
