using ShipperStation.Domain.Common;
using ShipperStation.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipperStation.Domain.Entities;
public class PackageStatusHistory : BaseAuditableEntity<int>
{
    public string? Name { get; set; }
    public string? Description { get; set; }

    [Column(TypeName = "nvarchar(24)")]
    public PackageStatus Status { get; set; }

    public Guid PackageId { get; set; }
    public virtual Package Package { get; set; } = default!;
}
