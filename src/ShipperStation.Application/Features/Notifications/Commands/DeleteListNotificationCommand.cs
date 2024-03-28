using MediatR;
using ShipperStation.Application.Models;

namespace ShipperStation.Application.Features.Notifications.Commands;
public sealed record DeleteListNotificationCommand : IRequest<MessageResponse>
{
    public IList<int> Ids { get; set; } = new List<int>();
}
