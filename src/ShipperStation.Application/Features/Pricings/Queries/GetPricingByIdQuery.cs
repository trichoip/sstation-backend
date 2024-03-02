using MediatR;
using ShipperStation.Application.Features.Pricings.Models;

namespace ShipperStation.Application.Features.Pricings.Queries;
public sealed record GetPricingByIdQuery(int Id) : IRequest<PricingResponse>
{
    public int StationId { get; set; }
}