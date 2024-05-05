using Mapster;
using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Racks.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Racks.Handlers;
internal sealed class UpdateRackCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateRackCommand, MessageResponse>
{
    private readonly IGenericRepository<Rack> _rackRepository = unitOfWork.Repository<Rack>();
    public async Task<MessageResponse> Handle(UpdateRackCommand request, CancellationToken cancellationToken)
    {
        var rack = await _rackRepository.FindByAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (rack is null)
        {
            throw new NotFoundException(nameof(Rack), request.Id);
        }

        if (await _rackRepository.ExistsByAsync(_ => _.Id != request.Id && _.ShelfId == rack.ShelfId && _.Name == request.Name, cancellationToken))
        {
            throw new ConflictException(nameof(Rack), request.Name);
        }

        request.Adapt(rack);
        await unitOfWork.CommitAsync(cancellationToken);

        return new MessageResponse(Resource.UpdatedSuccess);
    }
}
