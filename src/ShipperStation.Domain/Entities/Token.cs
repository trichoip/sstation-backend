using ShipperStation.Domain.Common;
using ShipperStation.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipperStation.Domain.Entities;
public class Token : BaseAuditableEntity<int>
{
    public string? Value { get; set; }
    public DateTimeOffset? ExpiredAt { get; set; }

    [Column(TypeName = "nvarchar(24)")]
    public TokenType Type { get; set; }

    [Column(TypeName = "nvarchar(24)")]
    public TokenStatus Status { get; set; }

    [Column(TypeName = "nvarchar(24)")]
    public DeviceType DeviceType { get; set; }

    public Guid UserId { get; set; }
    public virtual User User { get; set; } = default!;
}
