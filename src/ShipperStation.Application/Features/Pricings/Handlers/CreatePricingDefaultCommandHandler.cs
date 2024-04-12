using LinqKit;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.Pricings.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Pricings.Handlers;
internal sealed class CreatePricingDefaultCommandHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService) : IRequestHandler<CreatePricingDefaultCommand, MessageResponse>
{
    private readonly IGenericRepository<Station> _stationRepository = unitOfWork.Repository<Station>();
    private readonly IGenericRepository<DefaultPricing> _defaultPricingRepository = unitOfWork.Repository<DefaultPricing>();

    public async Task<MessageResponse> Handle(CreatePricingDefaultCommand request, CancellationToken cancellationToken)
    {
        var userId = await currentUserService.FindCurrentUserIdAsync();

        var station = await _stationRepository.FindByAsync(_ =>
                _.Id == request.StationId &&
                _.UserStations.Any(_ => _.UserId == userId),
               _ => _.Include(_ => _.Pricings),
               cancellationToken);

        if (station is null)
        {
            throw new NotFoundException(nameof(Station), request.StationId);
        }

        var defaultPricing = await _defaultPricingRepository.FindAsync(cancellationToken: cancellationToken);
        defaultPricing.ForEach(_ => _.Id = 0);

        var pricings = defaultPricing.Adapt<IList<Pricing>>();

        station.Pricings = pricings;

        await unitOfWork.CommitAsync(cancellationToken);

        return new MessageResponse(Resource.CreatedSuccess);
    }
}
