namespace ShipperStation.Application.Features.Sizes.Models;
public sealed record SizeResponse
{
    public int Id { get; set; } = default!;
    public string? Name { get; set; }
    public string? Description { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public double Length { get; set; }
    public double Volume { get; set; }
}
