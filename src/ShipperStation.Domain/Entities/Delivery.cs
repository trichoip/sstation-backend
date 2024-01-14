using ShipperStation.Domain.Common;
using ShipperStation.Domain.Entities.Identities;
using ShipperStation.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipperStation.Domain.Entities;
public class Delivery : BaseAuditableEntity<int>
{
    public string? Name { get; set; }
    public string? Description { get; set; }

    public double Price { get; set; }

    [Column(TypeName = "nvarchar(24)")]
    public DeliveryStatus? Status { get; set; }

    public Guid UserId { get; set; }
    public virtual User User { get; set; } = default!;

    public Guid PackageId { get; set; }
    public virtual Package Package { get; set; } = default!;

    public virtual ICollection<DeliveryHistory> DeliveryHistories { get; set; } = new HashSet<DeliveryHistory>();
    public virtual ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();
}
