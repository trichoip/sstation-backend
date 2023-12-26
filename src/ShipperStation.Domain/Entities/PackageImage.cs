using ShipperStation.Domain.Common;

namespace ShipperStation.Domain.Entities;
public class PackageImage : BaseAuditableEntity<int>
{
    public string ImageUrl { get; set; } = default!;

    public Guid PackageId { get; set; }
    public virtual Package Package { get; set; } = default!;
}
