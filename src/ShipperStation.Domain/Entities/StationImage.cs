using ShipperStation.Domain.Common;

namespace ShipperStation.Domain.Entities;
public class StationImage : BaseEntity<int>
{
    public string ImageUrl { get; set; } = default!;

    public int StationId { get; set; }
    public virtual Station Station { get; set; } = default!;
}
