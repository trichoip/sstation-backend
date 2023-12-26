using ShipperStation.Domain.Common;

namespace ShipperStation.Domain.Entities;
public class StationImage : BaseAuditableEntity<int>
{
    public string ImageUrl { get; set; } = default!;

    public int StationId { get; set; }
    public virtual Station Station { get; set; } = default!;
}
