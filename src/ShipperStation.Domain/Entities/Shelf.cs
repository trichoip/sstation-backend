using EntityFrameworkCore.Projectables;
using ShipperStation.Domain.Common;

namespace ShipperStation.Domain.Entities;
public class Shelf : BaseEntity<int>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Index { get; set; }

    [Projectable]
    public double Volume => Racks.SelectMany(_ => _.Slots).Sum(_ => _.Volume);

    [Projectable]
    public double VolumeUsed => Racks.SelectMany(_ => _.Slots).Sum(_ => _.VolumeUsed);

    [Projectable]
    public int Capacity => (int)Math.Floor((100 - (VolumeUsed / Volume) * 100));

    public int ZoneId { get; set; }
    public virtual Zone Zone { get; set; } = default!;

    public virtual ICollection<Rack> Racks { get; set; } = new HashSet<Rack>();

}
