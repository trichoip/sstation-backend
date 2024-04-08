using MediatR;
using ShipperStation.Application.Common.Enums;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Racks.Models;
using ShipperStation.Application.Features.Racks.Queries;
using ShipperStation.Domain.Entities;
using ShipperStation.Shared.Pages;

namespace ShipperStation.Application.Features.Racks.Handlers;
internal sealed class GetRacksQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetRacksQuery, PaginatedResponse<RackResponse>>
{
    private readonly IGenericRepository<Rack> _rackRepository = unitOfWork.Repository<Rack>();
    public async Task<PaginatedResponse<RackResponse>> Handle(GetRacksQuery request, CancellationToken cancellationToken)
    {
        request = request with
        {
            SortDir = SortDirection.Asc,
            SortColumn = nameof(Rack.Index),
        };

        var racks = await _rackRepository
            .FindAsync<RackResponse>(
                request.PageIndex,
                request.PageSize,
                request.GetExpressions(),
                request.GetOrder(),
                cancellationToken);

        return await racks.ToPaginatedResponseAsync();
    }
}
