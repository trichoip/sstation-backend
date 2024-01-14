using ShipperStation.Domain.Common;
using ShipperStation.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipperStation.Domain.Entities;
public class DeliveryHistory : BaseAuditableEntity<int>
{
    public string? Name { get; set; }
    public string? Description { get; set; }

    [Column(TypeName = "nvarchar(24)")]
    public DeliveryStatus? Status { get; set; }

    public int DeliveryId { get; set; }
    public virtual Delivery Delivery { get; set; } = default!;
}
