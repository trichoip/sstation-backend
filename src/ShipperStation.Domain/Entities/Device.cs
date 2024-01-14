using ShipperStation.Domain.Common;
using ShipperStation.Domain.Entities.Identities;

namespace ShipperStation.Domain.Entities;
public class Device : BaseEntity<int>
{

    public string Token { get; set; } = default!;

    public Guid UserId { get; set; }
    public virtual User User { get; set; } = default!;
}
