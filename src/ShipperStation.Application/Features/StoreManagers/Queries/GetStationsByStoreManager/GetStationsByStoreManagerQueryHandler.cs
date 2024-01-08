using MediatR;
using ShipperStation.Application.Contracts.Stations;
using ShipperStation.Application.Interfaces.Repositories;
using ShipperStation.Application.Interfaces.Services;
using ShipperStation.Domain.Entities;
using ShipperStation.Shared.Pages;

namespace ShipperStation.Application.Features.StoreManagers.Queries.GetStationsByStoreManager;
internal sealed class GetStationsByStoreManagerQueryHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService) : IRequestHandler<GetStationsByStoreManagerQuery, PaginatedResponse<StationResponse>>
{
    private readonly IGenericRepository<Station> _stationRepository = unitOfWork.Repository<Station>();

    public async Task<PaginatedResponse<StationResponse>> Handle(
        GetStationsByStoreManagerQuery request,
        CancellationToken cancellationToken)
    {
        request = request with
        {
            UserId = await currentUserService.FindCurrentUserIdAsync()
        };

        var stations = await _stationRepository
            .FindAsync<StationResponse>(
                request.PageIndex,
                request.PageSize,
                request.GetExpressions(),
                request.GetOrder(),
                cancellationToken);

        return await stations.ToPaginatedResponseAsync();
    }
}