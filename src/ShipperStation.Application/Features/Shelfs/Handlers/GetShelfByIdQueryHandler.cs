using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Shelfs.Models;
using ShipperStation.Application.Features.Shelfs.Queries;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Shelfs.Handlers;
internal sealed class GetShelfByIdQueryHandler(
    IUnitOfWork unitOfWork) : IRequestHandler<GetShelfByIdQuery, ShelfResponse>
{
    private readonly IGenericRepository<Shelf> _shelfRepository = unitOfWork.Repository<Shelf>();

    public async Task<ShelfResponse> Handle(GetShelfByIdQuery request, CancellationToken cancellationToken)
    {
        var shelf = await _shelfRepository
            .FindByAsync<ShelfResponse>(x =>
                x.Id == request.Id,
            cancellationToken);

        if (shelf == null)
        {
            throw new NotFoundException(nameof(Shelf), request.Id);
        }

        return shelf;
    }
}
