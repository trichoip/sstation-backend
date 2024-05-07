using LinqKit;
using MediatR;
using ShipperStation.Application.Features.Transactions.Models;
using ShipperStation.Application.Models.Pages;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Enums;
using ShipperStation.Shared.Pages;
using System.Linq.Expressions;

namespace ShipperStation.Application.Features.Transactions.Queries;
public sealed record GetTransactionsForAdminQuery : PaginationRequest<Transaction>, IRequest<PaginatedResponse<TransactionResponseForAdmin>>
{
    public TransactionType? Type { get; set; }

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

    public Guid? UserId { get; set; }

    public string? PhoneNumber { get; set; }

    public override Expression<Func<Transaction, bool>> GetExpressions()
    {
        Expression = Expression.And(_ => !UserId.HasValue || _.UserId == UserId);
        Expression = Expression.And(_ => !From.HasValue || _.CreatedAt >= From);
        Expression = Expression.And(_ => !To.HasValue || _.CreatedAt <= To.Value.AddDays(1));
        Expression = Expression.And(_ => !Type.HasValue || _.Type == Type);
        Expression = Expression.And(_ => string.IsNullOrEmpty(PhoneNumber) || _.User.PhoneNumber == PhoneNumber);
        return Expression;
    }
}
