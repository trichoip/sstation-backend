using EntityFrameworkCore.Projectables;
using ShipperStation.Domain.Common;
using ShipperStation.Domain.Entities.Identities;
using ShipperStation.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipperStation.Domain.Entities;

public class Notification : BaseAuditableEntity<int>
{
    public string? Title { get; set; }
    public string? Content { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    public NotificationType Type { get; set; }

    [Column(TypeName = "nvarchar(24)")]
    public NotificationLevel Level { get; set; }

    [Projectable]
    public bool IsRead => ReadAt.HasValue;
    public DateTimeOffset? ReadAt { get; set; }

    public string? Data { get; set; }

    public Guid UserId { get; set; }
    public virtual User User { get; set; } = default!;

}