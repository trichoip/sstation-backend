using MediatR;
using ShipperStation.Application.Models;
using System.Text.Json.Serialization;

namespace ShipperStation.Application.Features.Notifications.Commands;
public sealed record UpdateNotificationStatusCommand : IRequest<MessageResponse>
{
    [JsonIgnore]
    public int Id { get; set; }

    public bool IsRead { get; set; }
}
