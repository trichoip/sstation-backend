using MediatR;
using ShipperStation.Application.Contracts;
using System.Text.Json.Serialization;

namespace ShipperStation.Application.Features.Notifications.Commands.UpdateNotificationStatus;
public sealed record UpdateNotificationStatusCommand : IRequest<MessageResponse>
{
    [JsonIgnore]
    public int Id { get; set; }

    public bool IsRead { get; set; }
}
