using ShipperStation.Domain.Enums;

namespace ShipperStation.Application.Contracts.Notifications;
public record NotificationRequest : BaseAuditableEntityResponse<int>
{
    public string? Title { get; set; }
    public string Content { get; set; } = default!;
    public NotificationType Type { get; set; }
    public EntityType EntityType { get; set; }
    public string? ReferenceId { get; set; }
    public bool IsRead { get; set; }
    public DateTimeOffset? ReadAt { get; set; }
    public NotificationLevel Level { get; set; }
    public string? Data { get; set; }
    public Guid UserId { get; set; }
}
