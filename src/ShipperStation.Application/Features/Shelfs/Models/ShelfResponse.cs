using ShipperStation.Application.Features.Racks.Models;
using ShipperStation.Application.Features.Zones.Models;
using System.Text.Json.Serialization;

namespace ShipperStation.Application.Features.Shelfs.Models;
public sealed record ShelfResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Index { get; set; }

    public double VolumeUsed { get; set; }

    public int ZoneId { get; set; }
    public ZoneResponse Zone { get; set; } = default!;

    [JsonIgnore]
    public ICollection<RackResponse> Racks { get; set; } = new HashSet<RackResponse>();
    public ICollection<RackResponse> RackSorts => Racks.OrderBy(x => x.Index).ToList();
}
