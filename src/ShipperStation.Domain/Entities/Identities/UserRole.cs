using Microsoft.AspNetCore.Identity;

namespace ShipperStation.Domain.Entities.Identities;
public class UserRole : IdentityUserRole<Guid>
{
    public virtual Role Role { get; set; } = default!;
}
