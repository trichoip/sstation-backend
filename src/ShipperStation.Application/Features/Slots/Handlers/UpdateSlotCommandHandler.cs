using Mapster;
using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Slots.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Slots.Handlers;
internal sealed class UpdateSlotCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateSlotCommand, MessageResponse>
{
    private readonly IGenericRepository<Slot> _slotRepository = unitOfWork.Repository<Slot>();
    public async Task<MessageResponse> Handle(UpdateSlotCommand request, CancellationToken cancellationToken)
    {
        var slot = await _slotRepository.FindByAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (slot is null)
        {
            throw new NotFoundException(nameof(Slot), request.Id);
        }

        request.Adapt(slot);
        await unitOfWork.CommitAsync(cancellationToken);

        return new MessageResponse(Resource.UpdatedSuccess);
    }
}
