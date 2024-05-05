using ShipperStation.Domain.Common;

namespace ShipperStation.Domain.Entities;
public class Pricing : BaseEntity<int>
{
    public int StartTime { get; set; }
    public int EndTime { get; set; }
    public double Price { get; set; }

    public int StationId { get; set; }
    public Station Station { get; set; } = default!;
}
