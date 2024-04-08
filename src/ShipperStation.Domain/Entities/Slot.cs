using EntityFrameworkCore.Projectables;
using ShipperStation.Domain.Common;
using ShipperStation.Domain.Enums;

namespace ShipperStation.Domain.Entities;
public class Slot : BaseEntity<int>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Index { get; set; }

    public double Width { get; set; }
    public double Height { get; set; }
    public double Length { get; set; }
    public double Volume { get; set; }

    public bool? IsActive { get; set; }

    [Projectable]
    public int NumberOfPackages => Packages.Where(_ =>
        _.Status != PackageStatus.Returned && _.Status != PackageStatus.Completed).Count();

    [Projectable]
    public double VolumeUsed => Packages.Where(_ =>
        _.Status != PackageStatus.Returned && _.Status != PackageStatus.Completed).Sum(_ => _.Volume);

    [Projectable]
    public int Capacity => (int)Math.Floor((100 - (VolumeUsed / Volume) * 100));

    public int RackId { get; set; }
    public virtual Rack Rack { get; set; } = default!;

    public virtual ICollection<Package> Packages { get; set; } = new HashSet<Package>();

}
