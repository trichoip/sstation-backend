using MediatR;
using ShipperStation.Application.Features.DefaultPricings.Models;
using ShipperStation.Application.Models.Pages;
using ShipperStation.Domain.Entities;
using ShipperStation.Shared.Pages;
using System.Linq.Expressions;

namespace ShipperStation.Application.Features.DefaultPricings.Queries;
public sealed record GetDefaultPricingsQuery : PaginationRequest<DefaultPricing>, IRequest<PaginatedResponse<DefaultPricingResponse>>
{
    public override Expression<Func<DefaultPricing, bool>> GetExpressions()
    {
        return Expression;
    }
}
