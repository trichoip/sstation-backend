using ShipperStation.Domain.Common;
using ShipperStation.Domain.Enums;

namespace ShipperStation.Application.Features.PackageFeature.Events;
internal sealed record SendNotifyPackageEvent : BaseEvent
{
    public string? Data { get; set; }

    public NotificationType Type { get; set; }
}
