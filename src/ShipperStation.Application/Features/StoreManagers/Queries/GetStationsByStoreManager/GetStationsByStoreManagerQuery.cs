using LinqKit;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using ShipperStation.Application.Contracts.Pages;
using ShipperStation.Application.Contracts.Stations;
using ShipperStation.Domain.Entities;
using ShipperStation.Shared.Pages;
using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace ShipperStation.Application.Features.StoreManagers.Queries.GetStationsByStoreManager;
public sealed record GetStationsByStoreManagerQuery : PaginationRequest<Station>, IRequest<PaginatedResponse<StationResponse>>
{
    public string? Search { get; set; }

    [JsonIgnore] // for body
    [BindNever] // for query
    public Guid StoreManagerId { get; set; }

    public override Expression<Func<Station, bool>> GetExpressions()
    {
        if (!string.IsNullOrWhiteSpace(Search))
        {
            Search = Search.Trim();
            var queryExpression = PredicateBuilder.New<Station>();
            queryExpression.Or(sta => EF.Functions.Like(sta.Name, $"%{Search}%"));
            queryExpression.Or(sta => EF.Functions.Like(sta.Description, $"%{Search}%"));
            queryExpression.Or(sta => EF.Functions.Like(sta.ContactPhone, $"%{Search}%"));
            queryExpression.Or(sta => EF.Functions.Like(sta.Address, $"%{Search}%"));
            Expression = Expression.And(queryExpression);
        }

        Expression = Expression.And(sta => sta.StationUsers.Any(_ => _.UserId == StoreManagerId));

        return Expression;
    }
}
