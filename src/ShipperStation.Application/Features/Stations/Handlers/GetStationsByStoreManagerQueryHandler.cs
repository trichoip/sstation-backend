using MediatR;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.Stations.Models;
using ShipperStation.Application.Features.Stations.Queries;
using ShipperStation.Domain.Entities;
using ShipperStation.Shared.Pages;

namespace ShipperStation.Application.Features.Stations.Handlers;
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