using ShipperStation.Domain.Common;

namespace ShipperStation.Domain.Entities;
public class DataImage : BaseAuditableEntity<Guid>
{
    public byte[] Data { get; set; } = default!;
    public string? ContentType { get; set; }
    public string? FileName { get; set; }
    public string? Extension { get; set; }
    public string? TableName { get; set; }
    public string? ReferenceName { get; set; }
}
