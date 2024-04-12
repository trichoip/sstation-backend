using ShipperStation.Domain.Common;

namespace ShipperStation.Domain.Entities;
public class DefaultPricing : BaseEntity<int>
{
    public int FromDate { get; set; }
    public int ToDate { get; set; }
    public double Price { get; set; }
}
