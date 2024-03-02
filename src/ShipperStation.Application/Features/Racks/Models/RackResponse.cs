using ShipperStation.Application.Features.Slots.Models;

namespace ShipperStation.Application.Features.Racks.Models;
public sealed record RackResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Index { get; set; }
    public int ShelfId { get; set; }
    public ICollection<SlotResponse> Slots { get; set; } = new HashSet<SlotResponse>();
}
