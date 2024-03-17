namespace ShipperStation.Application.Features.PackageFeature.Models;
public sealed record PackageImageResponse
{
    public int Id { get; set; }
    public string ImageUrl { get; set; } = default!;
}
