using MediatR;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Pricings.Models;
using ShipperStation.Application.Features.Pricings.Queries;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Pricings.Handlers;
internal sealed class GetPricingsQueryHandler(
    IUnitOfWork unitOfWork) : IRequestHandler<GetPricingsQuery, IList<PricingResponse>>
{
    private readonly IGenericRepository<Pricing> _pricingRepository = unitOfWork.Repository<Pricing>();
    public async Task<IList<PricingResponse>> Handle(GetPricingsQuery request, CancellationToken cancellationToken)
    {
        return await _pricingRepository.FindAsync<PricingResponse>(x =>
                    x.StationId == request.StationId,
                cancellationToken: cancellationToken);
    }
}

