using MediatR;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.DefaultPricings.Models;
using ShipperStation.Application.Features.DefaultPricings.Queries;
using ShipperStation.Domain.Entities;
using ShipperStation.Shared.Pages;

namespace ShipperStation.Application.Features.DefaultPricings.Handlers;
internal sealed class GetDefaultPricingsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetDefaultPricingsQuery, PaginatedResponse<DefaultPricingResponse>>
{
    private readonly IGenericRepository<DefaultPricing> _defaultPricingRepository = unitOfWork.Repository<DefaultPricing>();
    public async Task<PaginatedResponse<DefaultPricingResponse>> Handle(GetDefaultPricingsQuery request, CancellationToken cancellationToken)
    {
        var defaultPricings = await _defaultPricingRepository
            .FindAsync<DefaultPricingResponse>(
                request.PageIndex,
                request.PageSize,
                request.GetExpressions(),
                request.GetOrder(),
                cancellationToken);

        return await defaultPricings.ToPaginatedResponseAsync();
    }
}

