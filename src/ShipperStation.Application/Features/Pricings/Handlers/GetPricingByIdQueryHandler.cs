using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Pricings.Models;
using ShipperStation.Application.Features.Pricings.Queries;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Pricings.Handlers;
internal sealed class GetPricingByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetPricingByIdQuery, PricingResponse>
{
    private readonly IGenericRepository<Pricing> _pricingRepository = unitOfWork.Repository<Pricing>();
    public async Task<PricingResponse> Handle(GetPricingByIdQuery request, CancellationToken cancellationToken)
    {
        var pricing = await _pricingRepository
            .FindByAsync<PricingResponse>(x => x.Id == request.Id, cancellationToken);

        if (pricing == null)
        {
            throw new NotFoundException(nameof(Pricing), request.Id);
        }

        return pricing;
    }
}
