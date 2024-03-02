using MediatR;
using ShipperStation.Application.Models;

namespace ShipperStation.Application.Features.Shelfs.Commands;
public sealed record CreateShelfCommand : IRequest<MessageResponse>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Index { get; set; }

    public double Width { get; set; }
    public double Height { get; set; }
    public double Length { get; set; }
    public double Volume => Width * Height * Length;

    public int ZoneId { get; set; }
    public int SizeId { get; set; }
    public int NumberOfShelves { get; set; }
    public int NumberOfSlots { get; set; }

}
