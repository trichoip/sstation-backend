namespace ShipperStation.Application.Features.PackageFeature.Models;
public sealed record CreatePackageImageRequest
{
    public string ImageUrl { get; set; } = default!;
}
