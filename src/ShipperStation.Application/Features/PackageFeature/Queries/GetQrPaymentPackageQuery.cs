using MediatR;
using ShipperStation.Application.Features.PackageFeature.Models;

namespace ShipperStation.Application.Features.PackageFeature.Queries;
public sealed record GetQrPaymentPackageQuery : IRequest<QrPaymentPackage>
{
    public Guid Id { get; set; }
}
