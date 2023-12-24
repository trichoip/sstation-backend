using ShipperStation.Domain.Common.Interfaces;

namespace ShipperStation.Domain.Common;
public class BaseAuditableEntity<TKey> : BaseEntity<TKey>, IEntity<TKey>, IAuditableEntity where TKey : IEquatable<TKey>
{
    public string? CreatedBy { get; set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTimeOffset? ModifiedAt { get; set; }
    public string? DeletedBy { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}
