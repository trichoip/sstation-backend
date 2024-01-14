using ShipperStation.Domain.Common;

namespace ShipperStation.Domain.Entities;
public class Shelf : BaseEntity<int>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Index { get; set; }

    public int RackId { get; set; }
    public virtual Rack Rack { get; set; } = default!;

    public virtual ICollection<Slot> Slots { get; set; } = new HashSet<Slot>();

}
