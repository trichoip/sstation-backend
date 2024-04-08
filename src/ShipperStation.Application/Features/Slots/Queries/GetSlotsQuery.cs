using LinqKit;
using MediatR;
using ShipperStation.Application.Features.Slots.Models;
using ShipperStation.Application.Models.Pages;
using ShipperStation.Domain.Entities;
using ShipperStation.Shared.Pages;
using System.Linq.Expressions;

namespace ShipperStation.Application.Features.Slots.Queries;
public sealed record GetSlotsQuery : PaginationRequest<Slot>, IRequest<PaginatedResponse<SlotResponse>>
{
    public int? ShelfId { get; set; }
    public int? RackId { get; set; }

    public override Expression<Func<Slot, bool>> GetExpressions()
    {
        Expression = Expression.And(x => !ShelfId.HasValue || x.Rack.ShelfId == ShelfId);
        Expression = Expression.And(x => !RackId.HasValue || x.RackId == RackId);

        return Expression;
    }
}
