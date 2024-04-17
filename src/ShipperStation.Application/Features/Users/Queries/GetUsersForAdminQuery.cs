using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShipperStation.Application.Features.Users.Models;
using ShipperStation.Application.Models.Pages;
using ShipperStation.Domain.Entities.Identities;
using ShipperStation.Domain.Enums;
using ShipperStation.Shared.Pages;
using System.Linq.Expressions;

namespace ShipperStation.Application.Features.Users.Queries;
public sealed record GetUsersForAdminQuery : PaginationRequest<User>, IRequest<PaginatedResponse<UserResponse>>
{
    public string? Search { get; set; }

    public bool? IsActive { get; set; }

    public RoleEnums? Role { get; set; }

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

        Expression = Expression.And(u => !IsActive.HasValue || u.IsActive == IsActive);
        Expression = Expression.And(u => !Role.HasValue || u.UserRoles.Any(ur => ur.Role.Name == Role.ToString()));

        return Expression;
    }
}
