using LinqKit;
using MediatR;
using ShipperStation.Application.Features.Racks.Models;
using ShipperStation.Application.Models.Pages;
using ShipperStation.Domain.Entities;
using ShipperStation.Shared.Pages;
using System.Linq.Expressions;

namespace ShipperStation.Application.Features.Racks.Queries;
public sealed record GetRacksQuery : PaginationRequest<Rack>, IRequest<PaginatedResponse<RackResponse>>
{
    public int ShelfId { get; set; }

    public override Expression<Func<Rack, bool>> GetExpressions()
    {
        Expression = Expression.And(x => x.ShelfId == ShelfId);

        return Expression;
    }
}
