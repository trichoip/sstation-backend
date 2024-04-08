namespace ShipperStation.Application.Features.PackageFeature.Models;
public sealed record UpdatePackageImageRequest
{
    public int Id { get; set; }
    public string ImageUrl { get; set; } = default!;
}
