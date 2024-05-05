using EntityFrameworkCore.Projectables;
using ShipperStation.Domain.Common;
using ShipperStation.Domain.Enums;

namespace ShipperStation.Domain.Entities;
public class Rack : BaseEntity<int>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Index { get; set; }
    public int ShelfId { get; set; }
    public virtual Shelf Shelf { get; set; } = default!;

    public virtual ICollection<Package> Packages { get; set; } = new HashSet<Package>();

    [Projectable]
    public int NumberOfPackages => Packages.Where(_ =>
        _.Status != PackageStatus.Returned && _.Status != PackageStatus.Completed).Count();

    [Projectable]
    public double VolumeUsed => Packages.Where(_ =>
        _.Status != PackageStatus.Returned && _.Status != PackageStatus.Completed).Sum(_ => _.Volume);

}
