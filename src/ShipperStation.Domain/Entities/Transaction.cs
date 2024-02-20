using ShipperStation.Domain.Common;
using ShipperStation.Domain.Entities.Identities;
using ShipperStation.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipperStation.Domain.Entities;
public class Transaction : BaseAuditableEntity<Guid>
{
    public string? Description { get; set; }
    public double Amount { get; set; }

    [Column(TypeName = "nvarchar(24)")]
    public TransactionStatus Status { get; set; }

    [Column(TypeName = "nvarchar(24)")]
    public TransactionType Type { get; set; }

    [Column(TypeName = "nvarchar(24)")]
    public TransactionMethod Method { get; set; }

    public Guid UserId { get; set; }
    public virtual User User { get; set; } = default!;
}
