using ShipperStation.Domain.Common;

namespace ShipperStation.Domain.Entities;
public class Rack : BaseEntity<int>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Index { get; set; }

    public int StationId { get; set; }
    public virtual Station Station { get; set; } = default!;

    public virtual ICollection<Shelf> Shelves { get; set; } = new HashSet<Shelf>();

}
