using MediatR;
using ShipperStation.Application.Features.Dashboards.Models;

namespace ShipperStation.Application.Features.Dashboards.Queries;
public sealed record GetUserCountByMonthQuery : IRequest<IList<UserCountByMonthResponse>>
{
    public int Year { get; init; }
}
