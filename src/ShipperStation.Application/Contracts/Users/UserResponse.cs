using ShipperStation.Domain.Enums;

namespace ShipperStation.Application.Contracts.Users;

public sealed record UserResponse : BaseAuditableEntityResponse<Guid>
{
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public string? PhoneNumber { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
    public string? FullName { get; set; }
    public string? AvatarUrl { get; set; }
    public UserStatus Status { get; set; }
    public bool IsDeleted { get; set; }
}
