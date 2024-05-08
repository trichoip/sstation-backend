using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShipperStation.Application.Features.Managers.Models;
using ShipperStation.Application.Models.Pages;
using ShipperStation.Domain.Constants;
using ShipperStation.Domain.Entities.Identities;
using ShipperStation.Shared.Pages;
using System.Linq.Expressions;

namespace ShipperStation.Application.Features.Managers.Queries;
public sealed record GetStoreManagersQuery : PaginationRequest<User>, IRequest<PaginatedResponse<ManagerResponse>>
{
    public string? Search { get; set; }
    public override Expression<Func<User, bool>> GetExpressions()
    {
        if (!string.IsNullOrWhiteSpace(Search))
        {
            Search = Search.Trim();
            Expression = Expression
                .And(u => EF.Functions.Like(u.PhoneNumber, $"%{Search}%"))
                .Or(u => EF.Functions.Like(u.FullName, $"%{Search}%"))
                .Or(u => EF.Functions.Like(u.UserName, $"%{Search}%"));
        }

        Expression = Expression.And(u => u.UserRoles.Any(_ => _.Role.Name == RoleName.StationManager));

        return Expression;
    }
}
