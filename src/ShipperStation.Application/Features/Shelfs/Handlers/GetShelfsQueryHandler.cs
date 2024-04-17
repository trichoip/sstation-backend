using MediatR;
using ShipperStation.Application.Common.Enums;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Shelfs.Models;
using ShipperStation.Application.Features.Shelfs.Queries;
using ShipperStation.Domain.Entities;
using ShipperStation.Shared.Pages;

namespace ShipperStation.Application.Features.Shelfs.Handlers;
internal sealed class GetShelfsQueryHandler(
    IUnitOfWork unitOfWork) : IRequestHandler<GetShelfsQuery, PaginatedResponse<ShelfResponse>>
{
    private readonly IGenericRepository<Shelf> _shelfRepository = unitOfWork.Repository<Shelf>();

    public async Task<PaginatedResponse<ShelfResponse>> Handle(GetShelfsQuery request, CancellationToken cancellationToken)
    {
        request = request with
        {
            SortDir = SortDirection.Asc,
            SortColumn = nameof(Shelf.Index),
        };

        var shelfs = await _shelfRepository
            .FindAsync<ShelfResponse>(
                request.PageIndex,
                request.PageSize,
                request.GetExpressions(),
                request.GetOrder(),
                cancellationToken);

        return await shelfs.ToPaginatedResponseAsync();
    }
}
