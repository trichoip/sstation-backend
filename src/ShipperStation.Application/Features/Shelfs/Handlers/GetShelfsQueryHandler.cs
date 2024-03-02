using MediatR;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.Shelfs.Models;
using ShipperStation.Application.Features.Shelfs.Queries;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Shelfs.Handlers;
internal sealed class GetShelfsQueryHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService) : IRequestHandler<GetShelfsQuery, IList<ShelfResponse>>
{
    private readonly IGenericRepository<Rack> _rackRepository = unitOfWork.Repository<Rack>();

    public async Task<IList<ShelfResponse>> Handle(GetShelfsQuery request, CancellationToken cancellationToken)
    {
        var userId = await currentUserService.FindCurrentUserIdAsync();

        //var racks = await _rackRepository
        //    .FindAsync<ShelfResponse>(
        //    x => x.ZoneId == request.ZoneId &&
        //         x.Zone.Station.UserStations.Any(_ => _.UserId == userId),
        //    cancellationToken: cancellationToken);

        //return racks;
        return null;
    }
}
