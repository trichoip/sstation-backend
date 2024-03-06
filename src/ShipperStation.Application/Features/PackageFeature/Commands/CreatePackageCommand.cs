using MediatR;
using ShipperStation.Application.Features.PackageFeature.Models;
using ShipperStation.Application.Models;

namespace ShipperStation.Application.Features.PackageFeature.Commands;
public sealed record CreatePackageCommand : IRequest<MessageResponse>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public double PriceCod { get; set; }
    public bool IsCod { get; set; }
    public double Weight { get; set; }
    public double Height { get; set; }
    public double Width { get; set; }
    public double Length { get; set; }
    public double Volume => Width * Height * Length;

    public int StationId { get; set; }

    public Guid SenderId { get; set; }
    public Guid ReceiverId { get; set; }

    public ICollection<CreatePackageImageRequest> PackageImages { get; set; } = new HashSet<CreatePackageImageRequest>();
}
