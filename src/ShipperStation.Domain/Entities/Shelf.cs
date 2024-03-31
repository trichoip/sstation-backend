using EntityFrameworkCore.Projectables;
using ShipperStation.Domain.Common;

namespace ShipperStation.Domain.Entities;
public class Shelf : BaseEntity<int>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Index { get; set; }

    public double Width { get; set; }
    public double Height { get; set; }
    public double Length { get; set; }
    public double Volume { get; set; }

    [Projectable]
    public double VolumeUsed => Racks.SelectMany(_ => _.Slots).Sum(_ => _.VolumeUsed);

    [Projectable]
    public int Capacity => (int)(100 - (VolumeUsed / Volume) * 100);

    public int ZoneId { get; set; }
    public virtual Zone Zone { get; set; } = default!;

    public virtual ICollection<Rack> Racks { get; set; } = new HashSet<Rack>();

}
