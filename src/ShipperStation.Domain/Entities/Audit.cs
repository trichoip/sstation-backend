using ShipperStation.Domain.Common;

namespace ShipperStation.Domain.Entities;

public class Audit : BaseAuditableEntity<int>
{
    public string? UserId { get; set; }

    public DateTime DateTime { get; set; }
    public string Type { get; set; } = default!;

    public string TableName { get; set; } = default!;

    public string? OldValues { get; set; }

    public string? NewValues { get; set; }

    public string? AffectedColumns { get; set; }

    public string PrimaryKey { get; set; } = default!;
}