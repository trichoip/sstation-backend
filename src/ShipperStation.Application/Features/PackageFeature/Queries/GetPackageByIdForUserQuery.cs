using MediatR;
using ShipperStation.Application.Features.PackageFeature.Models;

namespace ShipperStation.Application.Features.PackageFeature.Queries;
public sealed record GetPackageByIdForUserQuery(Guid Id) : IRequest<PackageResponse>;
