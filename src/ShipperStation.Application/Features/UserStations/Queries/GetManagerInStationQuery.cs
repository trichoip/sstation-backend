using MediatR;
using ShipperStation.Application.Features.Users.Models;

namespace ShipperStation.Application.Features.UserStations.Queries;
public sealed record GetManagerInStationQuery(Guid Id) : IRequest<UserResponse>
{
    public int StationId { get; set; }
}
