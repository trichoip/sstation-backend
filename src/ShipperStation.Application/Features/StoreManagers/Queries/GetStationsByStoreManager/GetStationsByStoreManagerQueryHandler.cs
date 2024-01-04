using MediatR;
using ShipperStation.Application.Contracts.Stations;
using ShipperStation.Application.Interfaces.Repositories;
using ShipperStation.Domain.Entities;
using ShipperStation.Shared.Pages;

namespace ShipperStation.Application.Features.StoreManagers.Queries.GetStationsByStoreManager;
internal sealed class GetStationsByStoreManagerQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetStationsByStoreManagerQuery, PaginatedResponse<StationResponse>>
{
    private readonly IGenericRepository<Station> _stationRepository = unitOfWork.Repository<Station>();

    public async Task<PaginatedResponse<StationResponse>> Handle(
        GetStationsByStoreManagerQuery request,
        CancellationToken cancellationToken)
    {
        var stations = await _stationRepository
            .FindAsync<StationResponse>(
                request.PageIndex,
                request.PageSize,
                request.GetExpressions(),
                request.GetOrder(),
                cancellationToken);

        return stations.ToPaginatedResponse();
    }
}