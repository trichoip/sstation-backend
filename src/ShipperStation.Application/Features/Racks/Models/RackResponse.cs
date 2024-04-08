using ShipperStation.Application.Features.Slots.Models;
using System.Text.Json.Serialization;

namespace ShipperStation.Application.Features.Racks.Models;
public sealed record RackResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Index { get; set; }
    public int ShelfId { get; set; }

    [JsonIgnore]
    public ICollection<SlotResponse> Slots { get; set; } = new HashSet<SlotResponse>();
    public ICollection<SlotResponse> SlotSorts => Slots.OrderBy(x => x.Index).ToList();
}
