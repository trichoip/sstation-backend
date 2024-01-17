using MediatR;
using ShipperStation.Application.Contracts;
using System.Text.Json.Serialization;

namespace ShipperStation.Application.Features.Sizes.Commands.UpdateSize;
public sealed record UpdateSizeCommand : IRequest<MessageResponse>
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public double Length { get; set; }
    public double Volume => Width * Height * Length;
}
