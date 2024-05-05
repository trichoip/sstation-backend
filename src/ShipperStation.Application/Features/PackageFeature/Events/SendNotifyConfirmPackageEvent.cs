using ShipperStation.Domain.Common;

namespace ShipperStation.Application.Features.PackageFeature.Events;
internal sealed record SendNotifyConfirmPackageEvent : BaseEvent
{
    public Guid ReceiverId { get; set; }
    public Guid PackageId { get; set; }
}
