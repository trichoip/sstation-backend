using MediatR;
using ShipperStation.Application.Contracts;

namespace ShipperStation.Application.Features.Sizes.Commands.CreateSize;
public sealed record CreateSizeCommand : IRequest<MessageResponse>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public double Length { get; set; }
    public double Volume => Width * Height * Length;

}
