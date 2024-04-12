using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.DefaultPricings.Models;
using ShipperStation.Application.Features.DefaultPricings.Queries;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.DefaultPricings.Handlers;
internal sealed class GetDefaultPricingByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetDefaultPricingByIdQuery, DefaultPricingResponse>
{
    private readonly IGenericRepository<DefaultPricing> _defaultPricingRepository = unitOfWork.Repository<DefaultPricing>();
    public async Task<DefaultPricingResponse> Handle(GetDefaultPricingByIdQuery request, CancellationToken cancellationToken)
    {
        var defaultPricing = await _defaultPricingRepository.FindByAsync<DefaultPricingResponse>(x => x.Id == request.Id, cancellationToken);

        if (defaultPricing == null)
        {
            throw new NotFoundException(nameof(DefaultPricing), request.Id);
        }

        return defaultPricing;
    }
}
