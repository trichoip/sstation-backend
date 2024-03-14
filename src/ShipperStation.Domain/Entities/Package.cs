using EntityFrameworkCore.Projectables;
using ShipperStation.Domain.Common;
using ShipperStation.Domain.Entities.Identities;
using ShipperStation.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipperStation.Domain.Entities;
public class Package : BaseAuditableEntity<Guid>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public double PriceCod { get; set; }
    public bool IsCod { get; set; }
    public string? Barcode { get; set; }

    [Column(TypeName = "nvarchar(24)")]
    public PackageStatus Status { get; set; }
    public double Weight { get; set; }
    public double Height { get; set; }
    public double Width { get; set; }
    public double Length { get; set; }
    public double Volume { get; set; }

    public int SlotId { get; set; }
    public virtual Slot Slot { get; set; } = default!;

    public Guid SenderId { get; set; }
    public virtual User Sender { get; set; } = default!;

    public Guid ReceiverId { get; set; }
    public virtual User Receiver { get; set; } = default!;

    [Projectable]
    public Station Station => Slot.Rack.Shelf.Zone.Station;

    public virtual ICollection<PackageImage> PackageImages { get; set; } = new HashSet<PackageImage>();

    public virtual ICollection<PackageStatusHistory> PackageStatusHistories { get; set; } = new HashSet<PackageStatusHistory>();

}
