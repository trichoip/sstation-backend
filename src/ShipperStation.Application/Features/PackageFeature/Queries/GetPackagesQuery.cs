using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShipperStation.Application.Features.PackageFeature.Models;
using ShipperStation.Application.Models.Pages;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Enums;
using ShipperStation.Shared.Pages;
using System.Linq.Expressions;

namespace ShipperStation.Application.Features.PackageFeature.Queries;

//TODO: không validate package có phải trong station không( lười làm )
public sealed record GetPackagesQuery : PaginationRequest<Package>, IRequest<PaginatedResponse<PackageResponse>>
{
    public string? Name { get; set; }
    public PackageStatus? Status { get; set; }
    public List<PackageStatus> Statuses { get; set; } = new List<PackageStatus>();

    /// <summary>
    /// Format for From is "yyyy-MM-dd" or "MM/dd/yyyy"
    /// </summary>
    /// <example>2021-02-25T00:00:00.000000+00:00</example>
    public DateTimeOffset? From { get; set; }

    /// <summary>
    /// Format for To is "yyyy-MM-dd" or "MM/dd/yyyy"
    /// </summary>
    /// <example>2029-03-25T00:00:00.000000+00:00</example>
    public DateTimeOffset? To { get; set; }

    public int? StationId { get; set; }
    public int? ZoneId { get; set; }
    public int? ShelfId { get; set; }
    public int? RackId { get; set; }
    public int? SlotId { get; set; }

    public Guid? SenderId { get; set; }

    public Guid? ReceiverId { get; set; }

    public double? CheckinFromDays { get; set; }
    public double? CheckinToDays { get; set; }

    public override Expression<Func<Package, bool>> GetExpressions()
    {
        Expression = Expression.And(_ => string.IsNullOrWhiteSpace(Name) || EF.Functions.Like(_.Name, $"%{Name}%"));
        Expression = Expression.And(_ => !Status.HasValue || _.Status == Status);
        Expression = Expression.And(_ => !From.HasValue || _.CreatedAt >= From);
        Expression = Expression.And(_ => !To.HasValue || _.CreatedAt <= To.Value.AddDays(1));

        Expression = Expression.And(_ => !StationId.HasValue || _.Slot.Rack.Shelf.Zone.StationId == StationId);
        Expression = Expression.And(_ => !ZoneId.HasValue || _.Slot.Rack.Shelf.ZoneId == ZoneId);
        Expression = Expression.And(_ => !ShelfId.HasValue || _.Slot.Rack.ShelfId == ShelfId);
        Expression = Expression.And(_ => !RackId.HasValue || _.Slot.RackId == RackId);
        Expression = Expression.And(_ => !SlotId.HasValue || _.SlotId == SlotId);

        Expression = Expression.And(_ => !SenderId.HasValue || _.SenderId == SenderId);
        Expression = Expression.And(_ => !ReceiverId.HasValue || _.ReceiverId == ReceiverId);

        Expression = Expression.And(_ => !CheckinFromDays.HasValue || _.CreatedAt <= DateTimeOffset.UtcNow.AddDays(-CheckinFromDays.Value));
        Expression = Expression.And(_ => !CheckinToDays.HasValue || _.CreatedAt >= DateTimeOffset.UtcNow.AddDays(-CheckinToDays.Value));

        Expression = Expression.And(_ => !Statuses.Any() || Statuses.Contains(_.Status));

        return Expression;
    }
}
