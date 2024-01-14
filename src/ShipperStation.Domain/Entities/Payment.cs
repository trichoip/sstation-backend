using ShipperStation.Domain.Common;
using ShipperStation.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipperStation.Domain.Entities;
public class Payment : BaseAuditableEntity<Guid>
{
    public decimal Amount { get; set; }
    public string? Content { get; set; }

    [Column(TypeName = "nvarchar(24)")]
    public PaymentMethod Method { get; set; }

    [Column(TypeName = "nvarchar(24)")]
    public PaymentStatus Status { get; set; }
    public string? TransactionId { get; set; }
    public string? TransactionReference { get; set; }
    public string? TransactionUrl { get; set; }
    public string? Qr { get; set; }
    public string? Deeplink { get; set; }
    public string? Description { get; set; }

    public int DeliveryId { get; set; }
    public virtual Delivery Delivery { get; set; } = default!;

}
