using LinqKit;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using ShipperStation.Application.Features.Zones.Models;
using ShipperStation.Application.Models.Pages;
using ShipperStation.Domain.Entities;
using ShipperStation.Shared.Pages;
using System.Linq.Expressions;

namespace ShipperStation.Application.Features.Zones.Queries;
public sealed record GetZonesQuery : PaginationRequest<Zone>, IRequest<PaginatedResponse<ZoneResponse>>
{
    public string? Search { get; set; }

    [BindNever]
    public int StationId { get; set; }

    [BindNever]
    public Guid UserId { get; set; }

    public override Expression<Func<Zone, bool>> GetExpressions()
    {
        if (!string.IsNullOrWhiteSpace(Search))
        {
            Search = Search.Trim();
            Expression = Expression
                .Or(sta => EF.Functions.Like(sta.Name, $"%{Search}%"))
                .Or(sta => EF.Functions.Like(sta.Description, $"%{Search}%"));
        }

        Expression = Expression.And(_ => _.StationId == StationId)
                               .And(_ => _.Station.UserStations.Any(_ => _.UserId == UserId));
        return Expression;
    }
}