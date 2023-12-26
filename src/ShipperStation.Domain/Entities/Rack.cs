using ShipperStation.Domain.Common;

namespace ShipperStation.Domain.Entities;
public class Rack : BaseAuditableEntity<int>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Index { get; set; }

    public int ZoneId { get; set; }
    public virtual Zone Zone { get; set; } = default!;

    public virtual ICollection<Shelf> Shelves { get; set; } = new HashSet<Shelf>();

}
