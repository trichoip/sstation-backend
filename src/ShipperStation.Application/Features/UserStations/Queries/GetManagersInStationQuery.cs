using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ShipperStation.Application.Features.Users.Models;
using ShipperStation.Application.Models.Pages;
using ShipperStation.Domain.Entities.Identities;
using ShipperStation.Shared.Pages;
using System.Linq.Expressions;

namespace ShipperStation.Application.Features.UserStations.Queries;
public sealed record GetManagersInStationQuery : PaginationRequest<User>, IRequest<PaginatedResponse<UserResponse>>
{
    public string? Search { get; set; }

    [BindNever]
    public int StationId { get; set; }

    public override Expression<Func<User, bool>> GetExpressions()
    {
        throw new NotImplementedException();
    }
}
