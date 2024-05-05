using MediatR;
using ShipperStation.Application.Features.PackageFeature.Models;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Enums;
using System.Text.Json.Serialization;

namespace ShipperStation.Application.Features.PackageFeature.Commands;
public sealed record UpdatePackageCommand : IRequest<MessageResponse>
{
    [JsonIgnore]
    public Guid Id { get; set; }

    public string? Name { get; set; }
    public string? Description { get; set; }

    public PackageStatus Status { get; set; }
    public double Weight { get; set; }
    public double Height { get; set; }
    public double Width { get; set; }
    public double Length { get; set; }
    public double Volume => Height * Width * Length;

    public string? Reason { get; set; }

    public ICollection<UpdatePackageImageRequest> PackageImages { get; set; } = new HashSet<UpdatePackageImageRequest>();
}
