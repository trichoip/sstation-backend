using ShipperStation.Domain.Common;
using ShipperStation.Domain.Entities.Identities;
using ShipperStation.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipperStation.Domain.Entities;
public class Token : BaseAuditableEntity<int>
{
    [Column(TypeName = "nvarchar(24)")]
    public TokenType Type { get; set; }

    public string Value { get; set; } = default!;

    public Guid UserId { get; set; }
    public virtual User User { get; set; } = default!;
}
