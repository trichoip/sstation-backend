using ShipperStation.Domain.Common;

namespace ShipperStation.Application.Features.PackageFeature.Events;
internal sealed record SendNotifyCancelPackageEvent : BaseEvent
{
    public Guid PackageId { get; set; }
    public Guid SenderId { get; set; }
    public Guid ReceiverId { get; set; }
}
