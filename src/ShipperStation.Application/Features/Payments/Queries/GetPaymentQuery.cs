using LinqKit;
using MediatR;
using ShipperStation.Application.Features.Payments.Models;
using ShipperStation.Application.Models.Pages;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Enums;
using ShipperStation.Shared.Pages;
using System.Linq.Expressions;

namespace ShipperStation.Application.Features.Payments.Queries;
public sealed record GetPaymentQuery : PaginationRequest<Payment>, IRequest<PaginatedResponse<PaymentResponse>>
{
    public PaymentStatus? Status { get; set; }
    public Guid? PackageId { get; set; }
    public int? StationId { get; set; }

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

    public List<int> StationIds { get; set; } = new List<int>();

    public PaymentType? PaymentType { get; set; }

    public override Expression<Func<Payment, bool>> GetExpressions()
    {
        Expression = Expression.And(_ => !Status.HasValue || _.Status == Status);
        Expression = Expression.And(_ => !From.HasValue || _.CreatedAt >= From);
        Expression = Expression.And(_ => !To.HasValue || _.CreatedAt <= To.Value.AddDays(1));

        Expression = Expression.And(_ => !PackageId.HasValue || _.PackageId == PackageId);
        Expression = Expression.And(_ => !StationId.HasValue || _.StationId == StationId);

        Expression = Expression.And(_ => !StationIds.Any() || StationIds.Contains(_.StationId));

        Expression = Expression.And(_ => !PaymentType.HasValue || _.Type == PaymentType);

        return Expression;
    }
}
