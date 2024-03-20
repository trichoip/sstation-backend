using LinqKit;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ShipperStation.Application.Features.Transactions.Models;
using ShipperStation.Application.Models.Pages;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Enums;
using ShipperStation.Shared.Pages;
using System.Linq.Expressions;

namespace ShipperStation.Application.Features.Transactions.Queries;
public sealed record GetTransactionsQuery : PaginationRequest<Transaction>, IRequest<PaginatedResponse<TransactionResponse>>
{
    public TransactionType? Type { get; set; }

    /// <summary>
    /// Format for From is "yyyy-MM-dd" or "MM/dd/yyyy"
    /// </summary>
    /// <example>2021-02-25</example>
    public DateTimeOffset? From { get; set; }

    /// <summary>
    /// Format for To is "yyyy-MM-dd" or "MM/dd/yyyy"
    /// </summary>
    /// <example>2029-03-25</example>
    public DateTimeOffset? To { get; set; }

    [BindNever]
    public Guid UserId { get; set; }

    public override Expression<Func<Transaction, bool>> GetExpressions()
    {
        Expression = Expression.And(_ => _.UserId == UserId);
        Expression = Expression.And(_ => !From.HasValue || _.CreatedAt >= From);
        Expression = Expression.And(_ => !To.HasValue || _.CreatedAt <= To.Value.AddDays(1));
        Expression = Expression.And(_ => !Type.HasValue || _.Type == Type);
        return Expression;
    }
}
