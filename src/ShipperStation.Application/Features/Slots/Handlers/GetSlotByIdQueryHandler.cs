using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Slots.Models;
using ShipperStation.Application.Features.Slots.Queries;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Slots.Handlers;
internal sealed class GetSlotByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetSlotByIdQuery, SlotResponse>
{
    private readonly IGenericRepository<Slot> _slotRepository = unitOfWork.Repository<Slot>();
    public async Task<SlotResponse> Handle(GetSlotByIdQuery request, CancellationToken cancellationToken)
    {
        var slot = await _slotRepository.FindByAsync<SlotResponse>(x => x.Id == request.Id, cancellationToken);

        if (slot == null)
        {
            throw new NotFoundException(nameof(Slot), request.Id);
        }

        return slot;
    }
}
