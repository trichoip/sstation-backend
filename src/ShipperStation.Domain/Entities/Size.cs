using ShipperStation.Domain.Common;

namespace ShipperStation.Domain.Entities;
public class Size : BaseAuditableEntity<int>
{
    public string Name { get; set; } = default!;
    public string Abbreviation { get; set; } = default!;
    public string? Description { get; set; }

    public double Width { get; set; }
    public double Height { get; set; }
    public double Length { get; set; }

    public ICollection<Slot> Slots { get; set; } = new HashSet<Slot>();

}
