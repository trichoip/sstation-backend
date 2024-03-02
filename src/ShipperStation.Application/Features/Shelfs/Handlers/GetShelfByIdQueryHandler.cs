using MediatR;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.Shelfs.Models;
using ShipperStation.Application.Features.Shelfs.Queries;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Shelfs.Handlers;
internal sealed class GetShelfByIdQueryHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService) : IRequestHandler<GetShelfByIdQuery, ShelfResponse>
{
    private readonly IGenericRepository<Rack> _rackRepository = unitOfWork.Repository<Rack>();

    public async Task<ShelfResponse> Handle(GetShelfByIdQuery request, CancellationToken cancellationToken)
    {
        var userId = await currentUserService.FindCurrentUserIdAsync();

        //var rack = await _rackRepository
        //    .FindByAsync<ShelfResponse>(
        //    x => x.Id == request.Id &&
        //         x.Zone.Station.UserStations.Any(_ => _.UserId == userId),
        //    cancellationToken);

        //if (rack == null)
        //{
        //    throw new NotFoundException(nameof(Rack), request.Id);
        //}

        //return rack;
        return null;
    }
}
