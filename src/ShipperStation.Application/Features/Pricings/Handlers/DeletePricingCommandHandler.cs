using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Pricings.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Pricings.Handlers;
internal sealed class DeletePricingCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeletePricingCommand, MessageResponse>
{
    private readonly IGenericRepository<Pricing> _pricingRepository = unitOfWork.Repository<Pricing>();
    public async Task<MessageResponse> Handle(DeletePricingCommand request, CancellationToken cancellationToken)
    {
        var pricing = await _pricingRepository
            .FindByAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (pricing == null)
        {
            throw new NotFoundException(nameof(Size), request.Id);
        }

        await _pricingRepository.DeleteAsync(pricing);

        await unitOfWork.CommitAsync();

        return new MessageResponse(Resource.DeletedSuccess);
    }
}
