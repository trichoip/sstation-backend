using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.PackageFeature.Models;
using ShipperStation.Application.Features.PackageFeature.Queries;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.PackageFeature.Handlers;
internal sealed class GetPackageByIdForUserQueryHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService) : IRequestHandler<GetPackageByIdForUserQuery, PackageResponse>
{
    private readonly IGenericRepository<Package> _packageRepository = unitOfWork.Repository<Package>();
    public async Task<PackageResponse> Handle(GetPackageByIdForUserQuery request, CancellationToken cancellationToken)
    {
        var userId = await currentUserService.FindCurrentUserIdAsync();

        var package = await _packageRepository
            .FindByAsync<PackageResponse>(_ =>
                _.Id == request.Id && (_.SenderId == userId || _.ReceiverId == userId),
            cancellationToken);

        if (package is null)
        {
            throw new NotFoundException(nameof(Package), request.Id);
        }

        return package;
    }
}
