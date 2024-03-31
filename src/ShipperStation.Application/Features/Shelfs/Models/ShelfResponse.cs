using ShipperStation.Application.Features.Racks.Models;
using ShipperStation.Application.Features.Zones.Models;

namespace ShipperStation.Application.Features.Shelfs.Models;
public sealed record ShelfResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Index { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public double Length { get; set; }
    public double Volume { get; set; }

    public int Capacity { get; set; }

    public double VolumeUsed { get; set; }

    public int ZoneId { get; set; }
    public ZoneResponse Zone { get; set; } = default!;
    public ICollection<RackResponse> Racks { get; set; } = new HashSet<RackResponse>();
}
