using ShipperStation.Application.Features.Pricings.Models;

namespace ShipperStation.Application.Features.Stations.Models;
public sealed record StationPricingResponse
{
    public int Id { get; set; }
    public double? CustomPrice { get; set; }
    public int PricingId { get; set; }
    public PricingResponse Pricing { get; set; } = default!;
}
