using ShipperStation.Domain.Common;
using ShipperStation.Domain.Enums;

namespace ShipperStation.Domain.Entities;
public class Slot : BaseEntity<int>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public double Length { get; set; }
    public int Index { get; set; }
    public int NumberOfPackages { get; set; }
    public SlotStatus Status { get; set; } = default!;

    public int ShelfId { get; set; }
    public virtual Shelf Shelf { get; set; } = default!;
    public virtual ICollection<Package> Packages { get; set; } = new HashSet<Package>();

}
