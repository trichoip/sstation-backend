using Mapster;
using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Pricings.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Pricings.Handlers;
internal sealed class UpdatePricingCommandHandler(
    IUnitOfWork unitOfWork) : IRequestHandler<UpdatePricingCommand, MessageResponse>
{
    private readonly IGenericRepository<Pricing> _pricingRepository = unitOfWork.Repository<Pricing>();
    private readonly IGenericRepository<Station> _stationRepository = unitOfWork.Repository<Station>();
    public async Task<MessageResponse> Handle(UpdatePricingCommand request, CancellationToken cancellationToken)
    {

        // TODO: lưu ý khi update object có child
        // trường hợp 1: không cho update child -> không có check exist child 
        //  - 1.a: có truyèn child về -> thì khi find phải có find childId = request.childId
        //  - 1.b: không truyền child về -> không cần find childId
        // trường hợp 2: cho update child -> phải check exist child trước và lúc find child để update thì không có find childId = request.childId (tại vì giá trị update và trên database khác nhau)

        var pricing = await _pricingRepository
            .FindByAsync(x =>
                x.Id == request.Id &&
                x.StationId == request.StationId,
            cancellationToken: cancellationToken);

        if (pricing == null)
        {
            throw new NotFoundException(nameof(Pricing), request.Id);
        }

        request.Adapt(pricing);

        await unitOfWork.CommitAsync(cancellationToken);

        return new MessageResponse(Resource.UpdatedSuccess);

    }
}
