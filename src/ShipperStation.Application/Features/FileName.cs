using MediatR;
using ShipperStation.Domain.Common;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features;

public sealed record ProductCreatedEvent(Wallet Product) : BaseEvent;

internal sealed class EmailHandler : INotificationHandler<ProductCreatedEvent>
{

    public async Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Email sent to {notification.Product.Id} for {notification.Product.Balance}");
    }
}