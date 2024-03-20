using LinqKit;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using ShipperStation.Application.Features.PackageFeature.Models;
using ShipperStation.Application.Models.Pages;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Enums;
using ShipperStation.Shared.Pages;
using System.Linq.Expressions;

namespace ShipperStation.Application.Features.PackageFeature.Queries;
public sealed record GetPackagesForUserQuery : PaginationRequest<Package>, IRequest<PaginatedResponse<PackageResponse>>
{
    public string? Name { get; set; }
    public PackageStatus? Status { get; set; }
    public PackageType? Type { get; set; }

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

    public override Expression<Func<Package, bool>> GetExpressions()
    {
        Expression = Expression.And(_ => string.IsNullOrWhiteSpace(Name) || EF.Functions.Like(_.Name, $"%{Name}%"));
        Expression = Expression.And(_ => !Status.HasValue || _.Status == Status);
        Expression = Expression.And(_ => !From.HasValue || _.CreatedAt >= From);
        Expression = Expression.And(_ => !To.HasValue || _.CreatedAt <= To.Value.AddDays(1));

        if (Type == PackageType.Sender)
        {
            Expression = Expression.And(_ => _.SenderId == UserId);
        }

        if (Type == PackageType.Receiver)
        {
            Expression = Expression.And(_ => _.ReceiverId == UserId);
        }

        if (!Type.HasValue)
        {
            Expression = Expression.And(_ => _.SenderId == UserId || _.ReceiverId == UserId);
        }

        return Expression;
    }
}