using MediatR;
using ShipperStation.Application.Features.Dashboards.Models;

namespace ShipperStation.Application.Features.Dashboards.Queries;
public sealed record GetInfomationDashBoardQuery : IRequest<IList<GetInfomationDashBoardModel>>
{
    public int? StationId { get; init; }
}
