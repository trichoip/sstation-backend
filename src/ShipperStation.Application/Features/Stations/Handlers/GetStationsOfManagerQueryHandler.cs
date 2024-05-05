using MediatR;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Stations.Models;
using ShipperStation.Application.Features.Stations.Queries;
using ShipperStation.Domain.Entities;
using ShipperStation.Shared.Pages;

namespace ShipperStation.Application.Features.Stations.Handlers;
internal sealed class GetStationsOfManagerQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetStationsOfManagerQuery, PaginatedResponse<StationResponse>>
{
    private readonly IGenericRepository<Station> _stationRepository = unitOfWork.Repository<Station>();
    public async Task<PaginatedResponse<StationResponse>> Handle(GetStationsOfManagerQuery request, CancellationToken cancellationToken)
    {
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
