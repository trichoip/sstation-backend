using ShipperStation.Domain.Common;

namespace ShipperStation.Application.Features.PackageFeature.Events;
public sealed record SendNotifyPackageExprireReceiveEvent : BaseEvent
{
    public Guid PackageId { get; set; }
}
