using MediatR;
using ShipperStation.Application.Models;

namespace ShipperStation.Application.Features.Shelfs.Commands;
public sealed record CreateShelfCommand : IRequest<MessageResponse>
{
    public int ZoneId { get; set; }
    public int NumberOfRacks { get; set; }
}
