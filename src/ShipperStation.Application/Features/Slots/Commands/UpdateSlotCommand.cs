using MediatR;
using ShipperStation.Application.Models;
using System.Text.Json.Serialization;

namespace ShipperStation.Application.Features.Slots.Commands;
public sealed record UpdateSlotCommand : IRequest<MessageResponse>
{
    [JsonIgnore]
    public int Id { get; set; }

    public string? Name { get; set; }
    public string? Description { get; set; }

    public double Width { get; set; }
    public double Height { get; set; }
    public double Length { get; set; }
    public double Volume => Width * Height * Length;

    public bool IsActive { get; set; }
}
