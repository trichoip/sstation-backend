using ShipperStation.Domain.Common;

namespace ShipperStation.Domain.Entities;
public class DefaultPricing : BaseEntity<int>
{
    public int StartTime { get; set; }
    public int EndTime { get; set; }
    public double Price { get; set; }
}
