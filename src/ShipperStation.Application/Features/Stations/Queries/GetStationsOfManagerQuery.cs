using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ShipperStation.Application.Features.Stations.Models;
using ShipperStation.Application.Models.Pages;
using ShipperStation.Domain.Entities;
using ShipperStation.Shared.Pages;
using System.Linq.Expressions;

namespace ShipperStation.Application.Features.Stations.Queries;
public sealed record GetStationsOfManagerQuery : PaginationRequest<Station>, IRequest<PaginatedResponse<StationResponse>>
{
    [BindNever]
    public Guid ManagerId { get; set; }
    public override Expression<Func<Station, bool>> GetExpressions()
    {
        return Expression;
    }
}
