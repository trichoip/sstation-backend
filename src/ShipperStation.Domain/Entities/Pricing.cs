using ShipperStation.Domain.Common;

namespace ShipperStation.Domain.Entities;
public class Pricing : BaseEntity<int>
{
    public int FromDate { get; set; }
    public int ToDate { get; set; }
    public double Price { get; set; }

    public virtual ICollection<StationPricing> StationPricings { get; set; } = new HashSet<StationPricing>();
}
