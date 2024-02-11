using Mapster;
using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Pricings.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Pricings.Handlers;
internal sealed class UpdatePricingCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdatePricingCommand, MessageResponse>
{
    private readonly IGenericRepository<Pricing> _pricingRepository = unitOfWork.Repository<Pricing>();
    public async Task<MessageResponse> Handle(UpdatePricingCommand request, CancellationToken cancellationToken)
    {
        var pricing = await _pricingRepository
            .FindByAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (pricing == null)
        {
            throw new NotFoundException(nameof(Pricing), request.Id);
        }

        request.Adapt(pricing);

        await unitOfWork.CommitAsync(cancellationToken);

        return new MessageResponse(Resource.UpdatedSuccess);

    }
}
