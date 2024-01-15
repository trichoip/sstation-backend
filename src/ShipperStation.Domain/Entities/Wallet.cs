using ShipperStation.Domain.Common;
using ShipperStation.Domain.Entities.Identities;

namespace ShipperStation.Domain.Entities;
public class Wallet : BaseAuditableEntity<int>
{
    public double Balance { get; set; }
    public DateTimeOffset? LastDepositAt { get; set; }

    public Guid UserId { get; set; }
    public virtual User User { get; set; } = default!;
}
