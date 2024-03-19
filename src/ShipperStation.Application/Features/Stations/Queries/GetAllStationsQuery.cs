using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShipperStation.Application.Features.Stations.Models;
using ShipperStation.Application.Models.Pages;
using ShipperStation.Domain.Entities;
using ShipperStation.Shared.Pages;
using System.Linq.Expressions;

namespace ShipperStation.Application.Features.Stations.Queries;
public sealed record GetAllStationsQuery : PaginationRequest<Station>, IRequest<PaginatedResponse<StationResponse>>
{
    public string? Search { get; set; }

    public override Expression<Func<Station, bool>> GetExpressions()
    {
        if (!string.IsNullOrWhiteSpace(Search))
        {
            Search = Search.Trim();
            Expression = Expression
                .And(sta => EF.Functions.Like(sta.Name, $"%{Search}%"))
                //.Or(sta => EF.Functions.Like(sta.Description, $"%{Search}%"))
                //.Or(sta => EF.Functions.Like(sta.ContactPhone, $"%{Search}%"))
                .Or(sta => EF.Functions.Like(sta.Address, $"%{Search}%"));
        }

        return Expression;
    }
}
