using MediatR;
using ShipperStation.Application.Models;

namespace ShipperStation.Application.Features.PackageFeature.Commands;
public sealed record ExpirePackageCommand : IRequest<MessageResponse>
{
    public List<Guid> Ids { get; set; } = new();
}
