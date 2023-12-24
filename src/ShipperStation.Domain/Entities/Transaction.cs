using ShipperStation.Domain.Common;
using ShipperStation.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipperStation.Domain.Entities;
public class Transaction : BaseAuditableEntity<int>
{
    public string? Description { get; set; }
    public decimal Amount { get; set; }

    [Column(TypeName = "nvarchar(24)")]
    public TransactionStatus Status { get; set; }

    [Column(TypeName = "nvarchar(24)")]
    public TransactionType Type { get; set; }

    public Guid UserId { get; set; }
    public virtual User User { get; set; } = default!;
}
