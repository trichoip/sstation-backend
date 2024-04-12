using Mapster;
using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Slots.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;
using ShipperStation.Shared.Extensions;

namespace ShipperStation.Application.Features.Slots.Handlers;
internal sealed class CreateSlotCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateSlotCommand, MessageResponse>
{
    private readonly IGenericRepository<Rack> _rackRepository = unitOfWork.Repository<Rack>();
    private readonly IGenericRepository<Slot> _slotRepository = unitOfWork.Repository<Slot>();
    public async Task<MessageResponse> Handle(CreateSlotCommand request, CancellationToken cancellationToken)
    {
        if (!await _rackRepository.ExistsByAsync(_ => _.Id == request.RackId, cancellationToken))
        {
            throw new NotFoundException(nameof(Rack), request.RackId);
        }

        var lastIndex = (await _slotRepository
            .FindAsync(_ => _.RackId == request.RackId, cancellationToken: cancellationToken)).MaxBy(_ => _.Index)?.Index;

        if (lastIndex is null)
        {
            lastIndex = 0;
        }

        var slot = request.Adapt<Slot>();
        slot.Index = lastIndex.Value + 1;
        slot.Name = slot.Index.GenerateNameIndex("S");
        slot.Description = $"Slot {slot.Index.GenerateNameIndex("S")}";

        await _slotRepository.CreateAsync(slot, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return new MessageResponse(Resource.CreatedSuccess);
    }
}
