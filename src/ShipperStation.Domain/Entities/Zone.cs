using ShipperStation.Domain.Common;

namespace ShipperStation.Domain.Entities;
public class Zone : BaseAuditableEntity<int>
{
    public string? Name { get; set; }
    public string? Description { get; set; }

    public int StationId { get; set; }
    public virtual Station Station { get; set; } = default!;

    public virtual ICollection<Rack> Racks { get; set; } = new HashSet<Rack>();

}
