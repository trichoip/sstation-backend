using ShipperStation.Domain.Common;
using ShipperStation.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipperStation.Domain.Entities;
public class Package : BaseAuditableEntity<Guid>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? Barcode { get; set; }
    public string? TrackingNumber { get; set; }
    public string OwnerName { get; set; } = default!;
    public string OwnerPhone { get; set; } = default!;
    public string? OwnerAddress { get; set; } = default!;
    public string SenderName { get; set; } = default!;
    public string SenderPhone { get; set; } = default!;

    [Column(TypeName = "nvarchar(24)")]
    public PackageStatus Status { get; set; }
    public string? Notes { get; set; }
    public string? PlatformName { get; set; }
    public double Weight { get; set; }
    public double Height { get; set; }
    public double Width { get; set; }
    public double Length { get; set; }

    public int SlotId { get; set; }
    public virtual Slot Slot { get; set; } = default!;
    public virtual Order Order { get; set; } = default!;
    public virtual ICollection<PackageImage> PackageImages { get; set; } = new HashSet<PackageImage>();

}
