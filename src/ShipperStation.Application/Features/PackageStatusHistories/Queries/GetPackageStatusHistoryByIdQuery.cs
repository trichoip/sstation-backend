using MediatR;
using ShipperStation.Application.Features.PackageStatusHistories.Models;

namespace ShipperStation.Application.Features.PackageStatusHistories.Queries;
public sealed record GetPackageStatusHistoryByIdQuery(int Id) : IRequest<PackageStatusHistoryResponse>;