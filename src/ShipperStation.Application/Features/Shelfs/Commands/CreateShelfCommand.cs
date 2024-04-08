using MediatR;
using ShipperStation.Application.Models;

namespace ShipperStation.Application.Features.Shelfs.Commands;
public sealed record CreateShelfCommand : IRequest<MessageResponse>
{
    public string? Name { get; set; }
    public string? Description { get; set; }

    public int ZoneId { get; set; }
    public int NumberOfRacks { get; set; }
    public int NumberOfSlotsPerRack { get; set; }

    public CreateSlotRequest Slot { get; set; } = default!;

}
