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

    [Column(TypeName = "nvarchar(24)")]
    public PackageStatus Status { get; set; }
    public double Weight { get; set; }
    public double Height { get; set; }
    public double Width { get; set; }
    public double Length { get; set; }
    public double Volume { get; set; }

    public string? Reason { get; set; }

    public int NotificationCount { get; set; }

    public int RackId { get; set; }
    public virtual Rack Rack { get; set; } = default!;

    public Guid ReceiverId { get; set; }
    public virtual User Receiver { get; set; } = default!;

    [Projectable]// không cần NotMapped (test chỉ cần migration mà không có thêm field của Station là oke, còn nếu ra field thì NotMapped)
    public Station Station => Rack.Shelf.Zone.Station;

    public double TotalDays => Math.Ceiling((DateTimeOffset.UtcNow - CreatedAt!.Value).TotalDays);

    [Projectable]
    [NotMapped]
    public IEnumerable<Pricing> Pricings => Station.Pricings;

    [Projectable]
    public string Location => $"{Rack.Shelf.Zone.Name} - {Rack.Shelf.Name} - {Rack.Name}";

    [Projectable]
    public Zone Zone => Rack.Shelf.Zone;

    [Projectable]
    public Shelf Shelf => Rack.Shelf;

    public virtual ICollection<PackageImage> PackageImages { get; set; } = new HashSet<PackageImage>();

    public virtual ICollection<PackageStatusHistory> PackageStatusHistories { get; set; } = new HashSet<PackageStatusHistory>();

    public virtual ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();

}
