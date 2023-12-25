using ShipperStation.Application.Contracts;
using ShipperStation.Domain.Enums;

namespace ShipperStation.Application.Contracts.Notifications;

public class WebNotification : BaseAuditableEntityResponse
{
    public long Id { get; set; }

    public Guid UserId { get; set; }

    public NotificationType Type { get; set; }

    public EntityType EntityType { get; set; }

    public string? ReferenceId { get; set; }

    public string Title { get; set; } = default!;

    public string Content { get; set; } = default!;

    public string? Data { get; set; }

    public DateTimeOffset? ReadAt { get; set; }

    public bool IsRead => ReadAt != null || Deleted;

    public NotificationLevel Level { get; set; }
}