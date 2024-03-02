using MediatR;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.Pricings.Models;
using ShipperStation.Application.Features.Pricings.Queries;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Pricings.Handlers;
internal sealed class GetPricingsQueryHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService) : IRequestHandler<GetPricingsQuery, IList<PricingResponse>>
{
    private readonly IGenericRepository<Pricing> _pricingRepository = unitOfWork.Repository<Pricing>();
    public async Task<IList<PricingResponse>> Handle(GetPricingsQuery request, CancellationToken cancellationToken)
    {
        var userId = await currentUserService.FindCurrentUserIdAsync();

        return await _pricingRepository.FindAsync<PricingResponse>(x =>
                    x.StationId == request.StationId &&
                    x.Station.UserStations.Any(_ => _.UserId == userId),
                cancellationToken: cancellationToken);
    }
}

