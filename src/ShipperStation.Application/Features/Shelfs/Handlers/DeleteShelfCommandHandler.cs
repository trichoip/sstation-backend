using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.Shelfs.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Shelfs.Handlers;
internal sealed class DeleteShelfCommandHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService) : IRequestHandler<DeleteShelfCommand, MessageResponse>
{
    private readonly IGenericRepository<Shelf> _shelfRepository = unitOfWork.Repository<Shelf>();
    public async Task<MessageResponse> Handle(DeleteShelfCommand request, CancellationToken cancellationToken)
    {
        var userId = await currentUserService.FindCurrentUserIdAsync();

        var shelf = await _shelfRepository
            .FindByAsync(x =>
                x.Id == request.Id &&
                x.Zone.Station.UserStations.Any(_ => _.UserId == userId),
             cancellationToken: cancellationToken);

        if (shelf is null)
        {
            throw new NotFoundException(nameof(Shelf), request.Id);
        }

        await _shelfRepository.DeleteAsync(shelf);
        await unitOfWork.CommitAsync(cancellationToken);

        return new MessageResponse(Resource.DeletedSuccess);
    }
}
