using LinqKit;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ShipperStation.Application.Features.Transactions.Models;
using ShipperStation.Application.Models.Pages;
using ShipperStation.Domain.Entities;
using ShipperStation.Shared.Pages;
using System.Linq.Expressions;

namespace ShipperStation.Application.Features.Transactions.Queries;
public sealed record GetTransactionsQuery : PaginationRequest<Transaction>, IRequest<PaginatedResponse<TransactionResponse>>
{

    [BindNever]
    public Guid UserId { get; set; }

    public override Expression<Func<Transaction, bool>> GetExpressions()
    {
        Expression = Expression.And(_ => _.UserId == UserId);
        return Expression;
    }
}
