using LinqKit;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using ShipperStation.Application.Contracts.Pages;
using ShipperStation.Application.Contracts.Users;
using ShipperStation.Domain.Constants;
using ShipperStation.Domain.Entities.Identities;
using ShipperStation.Shared.Pages;
using System.Linq.Expressions;

namespace ShipperStation.Application.Features.StoreManagers.Queries.GetStaffs;
public sealed record GetStaffsQuery : PaginationRequest<User>, IRequest<PaginatedResponse<UserResponse>>
{
    /// <summary>
    /// Search field is search for fullName or userName or email or phoneNumber
    /// </summary>
    public string? Search { get; set; }

    [BindNever]
    public int StationId { get; set; }

    public override Expression<Func<User, bool>> GetExpressions()
    {
        if (!string.IsNullOrWhiteSpace(Search))
        {
            Search = Search.Trim();
            Expression = Expression
                .And(u => EF.Functions.Like(u.FullName, $"%{Search}%"))
                .Or(u => EF.Functions.Like(u.UserName, $"%{Search}%"))
                .Or(u => EF.Functions.Like(u.Email, $"%{Search}%"))
                .Or(u => EF.Functions.Like(u.PhoneNumber, $"%{Search}%"));
        }

        Expression = Expression.And(u =>
            u.UserStations.Any(_ => _.StationId == StationId)
                //&& u.UserRoles.Any(_ => _.Role.Name == Roles.Staff)
                );

        return Expression;
    }
}

