using EntityFrameworkCore.Projectables;
using ShipperStation.Domain.Common;

namespace ShipperStation.Domain.Entities;
public class Shelf : BaseEntity<int>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Index { get; set; }

    [Projectable]
    public double VolumeUsed => Racks.Sum(_ => _.VolumeUsed);

    public int ZoneId { get; set; }
    public virtual Zone Zone { get; set; } = default!;

    public virtual ICollection<Rack> Racks { get; set; } = new HashSet<Rack>();

}
