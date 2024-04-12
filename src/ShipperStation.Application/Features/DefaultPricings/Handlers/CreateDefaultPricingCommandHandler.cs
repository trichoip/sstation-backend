using Mapster;
using MediatR;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.DefaultPricings.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.DefaultPricings.Handlers;
internal sealed class CreateDefaultPricingCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateDefaultPricingCommand, MessageResponse>
{
    private readonly IGenericRepository<DefaultPricing> _defaultPricingRepository = unitOfWork.Repository<DefaultPricing>();
    public async Task<MessageResponse> Handle(CreateDefaultPricingCommand request, CancellationToken cancellationToken)
    {
        var defaultPricing = request.Adapt<DefaultPricing>();
        await _defaultPricingRepository.CreateAsync(defaultPricing, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        return new MessageResponse(Resource.CreatedSuccess);
    }
}
