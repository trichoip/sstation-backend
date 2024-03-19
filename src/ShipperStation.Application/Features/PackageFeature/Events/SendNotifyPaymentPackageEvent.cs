using ShipperStation.Domain.Common;

namespace ShipperStation.Application.Features.PackageFeature.Events;
internal sealed record SendNotifyPaymentPackageEvent : BaseEvent
{
    public Guid PackageId { get; set; }
}