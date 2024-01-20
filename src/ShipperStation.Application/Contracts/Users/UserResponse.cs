using ShipperStation.Application.Contracts.Devices;
using ShipperStation.Application.Contracts.Wallets;

namespace ShipperStation.Application.Contracts.Users;

public sealed record UserResponse : BaseAuditableEntityResponse<Guid>
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
    public string[] Roles { get; set; } = Array.Empty<string>();
}
