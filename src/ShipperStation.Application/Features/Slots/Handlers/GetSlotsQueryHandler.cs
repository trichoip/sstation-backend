using MediatR;
using ShipperStation.Application.Common.Enums;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Slots.Models;
using ShipperStation.Application.Features.Slots.Queries;
using ShipperStation.Domain.Entities;
using ShipperStation.Shared.Pages;

namespace ShipperStation.Application.Features.Slots.Handlers;
internal sealed class GetSlotsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetSlotsQuery, PaginatedResponse<SlotResponse>>
{
    private readonly IGenericRepository<Slot> _slotRepository = unitOfWork.Repository<Slot>();
    public async Task<PaginatedResponse<SlotResponse>> Handle(GetSlotsQuery request, CancellationToken cancellationToken)
    {
        request = request with
        {
            SortDir = SortDirection.Asc,
            SortColumn = nameof(Slot.Index),
        };

        var slots = await _slotRepository
            .FindAsync<SlotResponse>(
                request.PageIndex,
                request.PageSize,
                request.GetExpressions(),
                request.GetOrder(),
                cancellationToken);

        return await slots.ToPaginatedResponseAsync();
    }
}
