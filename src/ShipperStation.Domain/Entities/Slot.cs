using ShipperStation.Domain.Common;

namespace ShipperStation.Domain.Entities;
public class Slot : BaseEntity<int>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Index { get; set; }
    public int NumberOfPackages { get; set; }
    public int ShelfId { get; set; }
    public virtual Shelf Shelf { get; set; } = default!;

    public virtual ICollection<Package> Packages { get; set; } = new HashSet<Package>();

}
