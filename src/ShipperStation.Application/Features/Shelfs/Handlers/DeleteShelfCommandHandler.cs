using MediatR;
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
    private readonly IGenericRepository<Rack> _rackRepository = unitOfWork.Repository<Rack>();
    public async Task<MessageResponse> Handle(DeleteShelfCommand request, CancellationToken cancellationToken)
    {
        var userId = await currentUserService.FindCurrentUserIdAsync();

        //var rack = await _rackRepository.FindByAsync(
        //    _ => _.Id == request.Id &&
        //         _.Zone.Station.UserStations.Any(_ => _.UserId == userId),
        //    cancellationToken: cancellationToken);

        //if (rack is null)
        //{
        //    throw new NotFoundException(nameof(Rack), request.Id);
        //}

        //await _rackRepository.DeleteAsync(rack);
        //await unitOfWork.CommitAsync(cancellationToken);

        return new MessageResponse(Resource.DeletedSuccess);
    }
}
