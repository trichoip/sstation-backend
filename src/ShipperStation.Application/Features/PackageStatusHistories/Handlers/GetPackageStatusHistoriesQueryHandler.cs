using MediatR;
using ShipperStation.Application.Common.Enums;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.PackageStatusHistories.Models;
using ShipperStation.Application.Features.PackageStatusHistories.Queries;
using ShipperStation.Domain.Entities;
using ShipperStation.Shared.Pages;

namespace ShipperStation.Application.Features.PackageStatusHistories.Handlers;
internal sealed class GetPackageStatusHistoriesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetPackageStatusHistoriesQuery, PaginatedResponse<PackageStatusHistoryResponse>>
{
    private readonly IGenericRepository<PackageStatusHistory> _packageStatusHistoryRepository = unitOfWork.Repository<PackageStatusHistory>();
    public async Task<PaginatedResponse<PackageStatusHistoryResponse>> Handle(GetPackageStatusHistoriesQuery request, CancellationToken cancellationToken)
    {
        request = request with
        {
            SortDir = SortDirection.Desc,
            SortColumn = nameof(Package.CreatedAt)
        };

        var packageStatusHistories = await _packageStatusHistoryRepository
            .FindAsync<PackageStatusHistoryResponse>(
                request.PageIndex,
                request.PageSize,
                request.GetExpressions(),
                request.GetOrder(),
                cancellationToken);

        return await packageStatusHistories.ToPaginatedResponseAsync();
    }
}
