using MediatR;
using ShipperStation.Application.Features.Pricings.Models;

namespace ShipperStation.Application.Features.Pricings.Queries;
public sealed record GetPricingsQuery : IRequest<IList<PricingResponse>>
{
    public int StationId { get; set; }
}