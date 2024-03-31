namespace ShipperStation.Application.Features.Slots.Models;
public sealed record SlotResponse
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

    public int Capacity { get; set; }

    public double VolumeUsed { get; set; }

    public int RackId { get; set; }
    //public ICollection<PackageResponse> Packages { get; set; } = new HashSet<PackageResponse>();
}
