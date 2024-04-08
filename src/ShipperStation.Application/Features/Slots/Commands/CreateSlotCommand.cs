using MediatR;
using ShipperStation.Application.Models;

namespace ShipperStation.Application.Features.Slots.Commands;
public sealed record CreateSlotCommand : IRequest<MessageResponse>
{
    public string? Name { get; set; }
    public string? Description { get; set; }

    public double Width { get; set; }
    public double Height { get; set; }
    public double Length { get; set; }

    public double Volume => Width * Height * Length;

    public bool IsActive { get; set; }

    public int RackId { get; set; }
}
