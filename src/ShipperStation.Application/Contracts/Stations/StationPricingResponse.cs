using ShipperStation.Application.Contracts.Pricings;

namespace ShipperStation.Application.Contracts.Stations;
public sealed record StationPricingResponse
{
    public int Id { get; set; }
    public double? CustomPrice { get; set; }
    public int PricingId { get; set; }
    public PricingResponse Pricing { get; set; } = default!;
}
