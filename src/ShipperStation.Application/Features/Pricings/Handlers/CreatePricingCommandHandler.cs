using Mapster;
using MediatR;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Pricings.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Pricings.Handlers;
internal sealed class CreatePricingCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreatePricingCommand, MessageResponse>
{
    private readonly IGenericRepository<Pricing> _pricingRepository = unitOfWork.Repository<Pricing>();
    public async Task<MessageResponse> Handle(CreatePricingCommand request, CancellationToken cancellationToken)
    {
        var pricing = request.Adapt<Pricing>();
        await _pricingRepository.CreateAsync(pricing);
        await unitOfWork.CommitAsync();

        return new MessageResponse(Resource.CreatedSuccess);
    }
}
