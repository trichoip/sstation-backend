namespace ShipperStation.Application.Features.Shelfs.Models;
public sealed record ShelfCreatePackageResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    public int ZoneId { get; set; }
}
