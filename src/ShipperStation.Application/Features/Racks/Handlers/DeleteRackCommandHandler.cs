using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Racks.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Racks.Handlers;
internal sealed class DeleteRackCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteRackCommand, MessageResponse>
{
    private readonly IGenericRepository<Rack> _rackRepository = unitOfWork.Repository<Rack>();
    public async Task<MessageResponse> Handle(DeleteRackCommand request, CancellationToken cancellationToken)
    {
        var rack = await _rackRepository.FindByAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (rack is null)
        {
            throw new NotFoundException(nameof(Rack), request.Id);
        }

        await _rackRepository.DeleteAsync(rack);
        await unitOfWork.CommitAsync(cancellationToken);

        return new MessageResponse(Resource.DeletedSuccess);
    }
}
