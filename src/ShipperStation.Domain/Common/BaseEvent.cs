using MediatR;

namespace ShipperStation.Domain.Common;
public abstract record BaseEvent : INotification
{
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
}

