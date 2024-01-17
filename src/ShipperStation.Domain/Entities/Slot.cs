using ShipperStation.Domain.Common;
using ShipperStation.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipperStation.Domain.Entities;
public class Slot : BaseEntity<int>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Index { get; set; }
    public int NumberOfPackages { get; set; }

    [Column(TypeName = "nvarchar(24)")]
    public SlotStatus Status { get; set; }

    public int ShelfId { get; set; }
    public virtual Shelf Shelf { get; set; } = default!;

    public virtual ICollection<Package> Packages { get; set; } = new HashSet<Package>();

}
