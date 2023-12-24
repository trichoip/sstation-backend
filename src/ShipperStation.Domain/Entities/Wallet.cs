using ShipperStation.Domain.Common;

namespace ShipperStation.Domain.Entities;
public class Wallet : BaseAuditableEntity<int>
{
    public decimal Balance { get; set; }
    public DateTimeOffset? LastDepositAt { get; set; }

    public Guid UserId { get; set; }
    public virtual User User { get; set; } = default!;
}
