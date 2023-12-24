using Microsoft.AspNetCore.Identity;

namespace ShipperStation.Domain.Entities;
public class UserRole : IdentityUserRole<Guid>
{
    public int StationId { get; set; }
    public virtual Station Station { get; set; } = default!;
}
