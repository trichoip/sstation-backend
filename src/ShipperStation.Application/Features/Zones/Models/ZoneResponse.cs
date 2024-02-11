namespace ShipperStation.Application.Features.Zones.Models;
public sealed record ZoneResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}
