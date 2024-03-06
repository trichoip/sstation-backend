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
public sealed record GetPackagesQuery : PaginationRequest<Package>, IRequest<PaginatedResponse<PackageResponse>>
{
    public string? Name { get; set; }
    public PackageStatus? Status { get; set; }
    public PackageType Type { get; set; }

    [BindNever]
    public Guid UserId { get; set; }

    public override Expression<Func<Package, bool>> GetExpressions()
    {
        Expression = Expression.And(_ => string.IsNullOrWhiteSpace(Name) || EF.Functions.Like(_.Name, $"%{Name}%"));
        Expression = Expression.And(_ => !Status.HasValue || _.Status == Status);

        if (Type == PackageType.Sender)
        {
            Expression = Expression.And(_ => _.SenderId == UserId);
        }

        if (Type == PackageType.Receiver)
        {
            Expression = Expression.And(_ => _.ReceiverId == UserId);
        }

        return Expression;
    }
}