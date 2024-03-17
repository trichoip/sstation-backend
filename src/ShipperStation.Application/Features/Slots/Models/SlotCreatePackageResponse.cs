namespace ShipperStation.Application.Features.Slots.Models;
public sealed record SlotCreatePackageResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Index { get; set; }

    public double Width { get; set; }
    public double Height { get; set; }
    public double Length { get; set; }
    public double Volume { get; set; }
    public int NumberOfPackages { get; set; }

    public int RackId { get; set; }
}
