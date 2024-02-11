using ShipperStation.Application.Features.Roles.Models;

namespace ShipperStation.Application.Features.Users.Models;
public sealed record UserRoleResponse
{
    public RoleResponse Role { get; set; } = default!;
}
