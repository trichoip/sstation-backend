using MediatR;
using ShipperStation.Application.Features.PackageFeature.Models;

namespace ShipperStation.Application.Features.PackageFeature.Queries;
public sealed record GetPackageByIdQuery(Guid Id) : IRequest<PackageResponse>;
