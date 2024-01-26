using ShipperStation.Domain.Common;

namespace ShipperStation.Domain.Entities;
public class StationPricing : BaseEntity<int>
{
    public double? CustomPrice { get; set; }

    public int PricingId { get; set; }
    public int StationId { get; set; }

    public Pricing Pricing { get; set; } = default!;
    public Station Station { get; set; } = default!;
}
