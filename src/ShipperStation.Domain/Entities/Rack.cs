using ShipperStation.Domain.Common;

namespace ShipperStation.Domain.Entities;
public class Rack : BaseEntity<int>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Index { get; set; }

    public int ShelfId { get; set; }
    public virtual Shelf Shelf { get; set; } = default!;
    public virtual ICollection<Slot> Slots { get; set; } = new HashSet<Slot>();

}
