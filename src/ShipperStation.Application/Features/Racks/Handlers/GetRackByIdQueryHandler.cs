using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Racks.Models;
using ShipperStation.Application.Features.Racks.Queries;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Racks.Handlers;
internal sealed class GetRackByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetRackByIdQuery, RackResponse>
{
    private readonly IGenericRepository<Rack> _rackRepository = unitOfWork.Repository<Rack>();
    public async Task<RackResponse> Handle(GetRackByIdQuery request, CancellationToken cancellationToken)
    {
        var rack = await _rackRepository.FindByAsync<RackResponse>(x => x.Id == request.Id, cancellationToken);

        if (rack == null)
        {
            throw new NotFoundException(nameof(Rack), request.Id);
        }

        return rack;
    }
}
