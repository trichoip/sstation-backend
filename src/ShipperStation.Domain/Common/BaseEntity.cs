using ShipperStation.Domain.Common.Interfaces;

namespace ShipperStation.Domain.Common;
public class BaseEntity<TKey> : BaseDomainEvent, IEntity<TKey> where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; } = default!;

}
