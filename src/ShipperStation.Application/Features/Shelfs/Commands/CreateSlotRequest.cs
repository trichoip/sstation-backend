namespace ShipperStation.Application.Features.Shelfs.Commands;
public sealed record CreateSlotRequest
{
    public double Width { get; set; }
    public double Height { get; set; }
    public double Length { get; set; }
    public double Volume => Width * Height * Length;
}
