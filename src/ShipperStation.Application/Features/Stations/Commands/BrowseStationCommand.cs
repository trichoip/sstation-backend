using MediatR;
using ShipperStation.Application.Models;
using System.Text.Json.Serialization;

namespace ShipperStation.Application.Features.Stations.Commands;
public sealed record BrowseStationCommand : IRequest<MessageResponse>
{
    [JsonIgnore]
    public int Id { get; init; }
    public bool IsActive { get; set; }
}
