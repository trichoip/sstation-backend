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

    [Projectable]// không cần NotMapped (test chỉ cần migration mà không có thêm field của Station là oke, còn nếu ra field thì NotMapped)
    public Station Station => Slot.Rack.Shelf.Zone.Station;

    public int TotalDays => (int)Math.Ceiling((DateTimeOffset.UtcNow - CreatedAt!.Value).TotalDays);

    [Projectable]
    [NotMapped]
    public IEnumerable<Pricing> Pricings => Station.Pricings;

    [Projectable]
    public string Location => $"{Slot.Rack.Shelf.Name} - {Slot.Rack.Name} - {Slot.Name}";

    [Projectable]
    public Zone Zone => Slot.Rack.Shelf.Zone;

    [Projectable]
    public Rack Rack => Slot.Rack;

    [Projectable]
    public Shelf Shelf => Slot.Rack.Shelf;

    public virtual ICollection<PackageImage> PackageImages { get; set; } = new HashSet<PackageImage>();

    public virtual ICollection<PackageStatusHistory> PackageStatusHistories { get; set; } = new HashSet<PackageStatusHistory>();

}
