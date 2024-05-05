using Mapster;
using MediatR;
using ShipperStation.Application.Common.Exceptions;
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
        var exists = await _defaultPricingRepository
            .ExistsByAsync(_ => request.StartTime >= _.StartTime && request.StartTime <= _.EndTime ||
                                request.EndTime >= _.StartTime && request.EndTime <= _.EndTime ||
                                _.StartTime >= request.StartTime && _.StartTime <= request.EndTime ||
                                _.EndTime >= request.StartTime && _.EndTime <= request.EndTime, cancellationToken);

        if (exists)
        {
            throw new ConflictException($"Pricing existed during the {request.StartTime - request.EndTime} period");
        }

        var defaultPricing = request.Adapt<DefaultPricing>();
        await _defaultPricingRepository.CreateAsync(defaultPricing, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        return new MessageResponse(Resource.CreatedSuccess);
    }
}
