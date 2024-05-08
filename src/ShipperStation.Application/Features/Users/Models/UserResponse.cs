using ShipperStation.Application.Features.Devices.Models;
using ShipperStation.Application.Features.Roles.Models;
using ShipperStation.Application.Features.Wallets.Models;
using ShipperStation.Application.Models;

namespace ShipperStation.Application.Features.Users.Models;

public record UserResponse : BaseAuditableEntityResponse<Guid>
{
    public string? UserName { get; set; }
    public string? AvatarUrl { get; set; }
    public string? Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public string? PhoneNumber { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
    public string? FullName { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }

    public WalletResponse Wallet { get; set; } = default!;

    public ICollection<DeviceResponse> Devices { get; set; } = new HashSet<DeviceResponse>();
    public ICollection<RoleResponse> Roles { get; set; } = new HashSet<RoleResponse>();
}
