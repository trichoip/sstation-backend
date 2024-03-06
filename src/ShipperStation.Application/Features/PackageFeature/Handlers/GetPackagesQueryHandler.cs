using MediatR;
using ShipperStation.Application.Common.Enums;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.PackageFeature.Models;
using ShipperStation.Application.Features.PackageFeature.Queries;
using ShipperStation.Domain.Entities;
using ShipperStation.Shared.Pages;

namespace ShipperStation.Application.Features.PackageFeature.Handlers;
internal sealed class GetPackagesQueryHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService) : IRequestHandler<GetPackagesQuery, PaginatedResponse<PackageResponse>>
{
    private readonly IGenericRepository<Package> _packageRepository = unitOfWork.Repository<Package>();
    public async Task<PaginatedResponse<PackageResponse>> Handle(GetPackagesQuery request, CancellationToken cancellationToken)
    {
        var userId = await currentUserService.FindCurrentUserIdAsync();

        request = request with
        {
            UserId = userId,
            SortDir = SortDirection.Desc,
            SortColumn = nameof(Package.CreatedAt)
        };

        var packages = await _packageRepository
            .FindAsync<PackageResponse>(
                request.PageIndex,
                request.PageSize,
                request.GetExpressions(),
                request.GetOrder(),
                cancellationToken);

        return await packages.ToPaginatedResponseAsync();
    }
}
