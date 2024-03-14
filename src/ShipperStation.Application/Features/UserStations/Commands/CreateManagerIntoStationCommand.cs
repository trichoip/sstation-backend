using MediatR;
using ShipperStation.Application.Models;
using System.Text.Json.Serialization;

namespace ShipperStation.Application.Features.UserStations.Commands;
public sealed record CreateManagerIntoStationCommand : IRequest<MessageResponse>
{
    [JsonIgnore]
    public int StationId { get; set; }

    public Guid UserId { get; set; }
}
