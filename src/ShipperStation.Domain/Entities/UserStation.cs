using ShipperStation.Domain.Entities.Identities;

namespace ShipperStation.Domain.Entities;
public class UserStation
{
    public Guid UserId { get; set; }
    public virtual User User { get; set; } = default!;

    public int StationId { get; set; }
    public virtual Station Station { get; set; } = default!;
}
