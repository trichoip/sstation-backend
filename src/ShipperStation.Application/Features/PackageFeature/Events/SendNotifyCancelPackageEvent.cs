using ShipperStation.Domain.Common;

namespace ShipperStation.Application.Features.PackageFeature.Events;
internal sealed record SendNotifyCancelPackageEvent : BaseEvent
{
    public Guid PackageId { get; set; }
}
