using Mapster;
using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.DefaultPricings.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.DefaultPricings.Handlers;
internal sealed class UpdateDefaultPricingCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateDefaultPricingCommand, MessageResponse>
{
    private readonly IGenericRepository<DefaultPricing> _defaultPricingRepository = unitOfWork.Repository<DefaultPricing>();
    public async Task<MessageResponse> Handle(UpdateDefaultPricingCommand request, CancellationToken cancellationToken)
    {
        var defaultPricing = await _defaultPricingRepository.FindByAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (defaultPricing is null)
        {
            throw new NotFoundException(nameof(DefaultPricing), request.Id);
        }

        request.Adapt(defaultPricing);
        await unitOfWork.CommitAsync(cancellationToken);

        return new MessageResponse(Resource.UpdatedSuccess);
    }
}
