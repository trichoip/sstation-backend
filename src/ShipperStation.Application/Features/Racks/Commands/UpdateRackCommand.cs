using MediatR;
using ShipperStation.Application.Models;
using System.Text.Json.Serialization;

namespace ShipperStation.Application.Features.Racks.Commands;
public sealed record UpdateRackCommand : IRequest<MessageResponse>
{
    [JsonIgnore]
    public int Id { get; set; }

    public string? Name { get; set; }
    public string? Description { get; set; }
}
