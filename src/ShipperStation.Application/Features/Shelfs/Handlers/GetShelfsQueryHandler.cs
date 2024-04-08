using MediatR;
using ShipperStation.Application.Common.Enums;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.Shelfs.Models;
using ShipperStation.Application.Features.Shelfs.Queries;
using ShipperStation.Domain.Entities;
using ShipperStation.Shared.Pages;

namespace ShipperStation.Application.Features.Shelfs.Handlers;
internal sealed class GetShelfsQueryHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService) : IRequestHandler<GetShelfsQuery, PaginatedResponse<ShelfResponse>>
{
    private readonly IGenericRepository<Shelf> _shelfRepository = unitOfWork.Repository<Shelf>();

    public async Task<PaginatedResponse<ShelfResponse>> Handle(GetShelfsQuery request, CancellationToken cancellationToken)
    {
        var userId = await currentUserService.FindCurrentUserIdAsync();
        request = request with
        {
            SortDir = SortDirection.Asc,
            SortColumn = nameof(Shelf.Index),
            UserId = userId
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
