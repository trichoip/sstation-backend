using ShipperStation.Domain.Common;
using ShipperStation.Domain.Entities.Identities;
using ShipperStation.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipperStation.Domain.Entities;

public class Notification : BaseAuditableEntity<int>
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public bool IsRead { get; set; }

    [Column(TypeName = "nvarchar(24)")]
    public NotificationType Type { get; set; }

    public string? ReferenceId { get; set; }
    public DateTimeOffset? ReadAt { get; set; }

    [Column(TypeName = "nvarchar(24)")]
    public NotificationLevel Level { get; set; }
    public Guid UserId { get; set; }
    public virtual User User { get; set; } = default!;

}