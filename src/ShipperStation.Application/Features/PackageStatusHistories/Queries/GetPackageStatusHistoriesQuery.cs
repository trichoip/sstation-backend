using LinqKit;
using MediatR;
using ShipperStation.Application.Features.PackageStatusHistories.Models;
using ShipperStation.Application.Models.Pages;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Enums;
using ShipperStation.Shared.Pages;
using System.Linq.Expressions;

namespace ShipperStation.Application.Features.PackageStatusHistories.Queries;
public sealed record GetPackageStatusHistoriesQuery : PaginationRequest<PackageStatusHistory>, IRequest<PaginatedResponse<PackageStatusHistoryResponse>>
{
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
    public Guid? PackageId { get; set; }

    public override Expression<Func<PackageStatusHistory, bool>> GetExpressions()
    {
        Expression = Expression.And(_ => !Status.HasValue || _.Status == Status);
        Expression = Expression.And(_ => !From.HasValue || _.CreatedAt >= From);
        Expression = Expression.And(_ => !To.HasValue || _.CreatedAt <= To.Value.AddDays(1));
        Expression = Expression.And(_ => !Statuses.Any() || Statuses.Contains(_.Status));

        Expression = Expression.And(_ => !StationId.HasValue || _.Package.Station.Id == StationId);
        Expression = Expression.And(_ => !PackageId.HasValue || _.PackageId == PackageId);

        return Expression;
    }
}
