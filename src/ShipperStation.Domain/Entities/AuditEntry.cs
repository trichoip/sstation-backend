using Microsoft.EntityFrameworkCore.ChangeTracking;
using ShipperStation.Domain.Enums;
using System.Text.Json;

namespace ShipperStation.Domain.Entities;

public class AuditEntry
{
    public AuditEntry(EntityEntry entry)
    {
        Entry = entry;
    }

    public EntityEntry Entry { get; } = default!;
    public string? UserId { get; set; } = default!;

    public string TableName { get; set; } = default!;

    public Dictionary<string, object> KeyValues { get; } = new();

    public Dictionary<string, object> OldValues { get; } = new();

    public Dictionary<string, object> NewValues { get; } = new();

    public AuditType AuditType { get; set; }

    public List<string> ChangedColumns { get; } = new();

    public Audit ToAudit()
    {

        var audit = new Audit();
        audit.UserId = UserId;
        audit.TableName = TableName;
        audit.Type = AuditType.ToString();
        audit.DateTime = DateTime.Now;

        audit.PrimaryKey = JsonSerializer.Serialize(KeyValues);
        audit.OldValues = OldValues.Any() ? JsonSerializer.Serialize(OldValues) : null;
        audit.NewValues = NewValues.Any() ? JsonSerializer.Serialize(NewValues) : null;
        audit.AffectedColumns = ChangedColumns.Any() ? JsonSerializer.Serialize(ChangedColumns) : null;
        audit.CreatedAt = DateTimeOffset.UtcNow;
        audit.ModifiedAt = DateTimeOffset.UtcNow;
        return audit;
    }
}