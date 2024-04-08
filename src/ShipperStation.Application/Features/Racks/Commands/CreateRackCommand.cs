using MediatR;
using ShipperStation.Application.Models;

namespace ShipperStation.Application.Features.Racks.Commands;
public sealed record CreateRackCommand : IRequest<MessageResponse>
{
    public string? Name { get; set; }
    public string? Description { get; set; }

    public int ShelfId { get; set; }
}
