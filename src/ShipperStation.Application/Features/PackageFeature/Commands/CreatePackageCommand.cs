using MediatR;
using ShipperStation.Application.Features.PackageFeature.Models;
using System.ComponentModel;

namespace ShipperStation.Application.Features.PackageFeature.Commands;
public sealed record CreatePackageCommand : IRequest<PackageResponse>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public double Weight { get; set; }
    public double Height { get; set; }
    public double Width { get; set; }
    public double Length { get; set; }
    public double Volume => Width * Height * Length;

    public int StationId { get; set; }

    public int ZoneId { get; set; }

    [DefaultValue("null")]
    public int? ShelfId { get; set; }

    [DefaultValue("null")]
    public int? RackId { get; set; }
    public Guid ReceiverId { get; set; }

    public ICollection<CreatePackageImageRequest> PackageImages { get; set; } = new HashSet<CreatePackageImageRequest>();
}
