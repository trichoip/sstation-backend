using LinqKit;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using ShipperStation.Application.Features.Shelfs.Models;
using ShipperStation.Application.Models.Pages;
using ShipperStation.Domain.Entities;
using ShipperStation.Shared.Pages;
using System.Linq.Expressions;

namespace ShipperStation.Application.Features.Shelfs.Queries;
public sealed record GetShelfsQuery : PaginationRequest<Shelf>, IRequest<PaginatedResponse<ShelfResponse>>
{
    [BindNever]
    public int ZoneId { get; init; }

    [BindNever]
    public Guid UserId { get; set; }

    public string? Search { get; set; }

    public override Expression<Func<Shelf, bool>> GetExpressions()
    {
        if (!string.IsNullOrWhiteSpace(Search))
        {
            Search = Search.Trim();
            Expression = Expression.And(sta => EF.Functions.Like(sta.Name, $"%{Search}%"));
        }

        Expression = Expression.And(x =>
           x.ZoneId == ZoneId &&
           x.Zone.Station.UserStations.Any(_ => _.UserId == UserId));

        return Expression;
    }
}