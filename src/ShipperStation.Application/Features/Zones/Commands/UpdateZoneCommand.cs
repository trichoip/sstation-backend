using MediatR;
using ShipperStation.Application.Models;
using System.Text.Json.Serialization;

namespace ShipperStation.Application.Features.Zones.Commands;
public sealed record UpdateZoneCommand : IRequest<MessageResponse>
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }

    [JsonIgnore]
    public int StationId { get; set; }
}
