using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.PackageFeature.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Enums;

namespace ShipperStation.Application.Features.PackageFeature.Handlers;
internal sealed class UpdateLocationPackageCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateLocationPackageCommand, MessageResponse>
{
    private readonly IGenericRepository<Package> _packageRepository = unitOfWork.Repository<Package>();
    private readonly IGenericRepository<Slot> _slotRepository = unitOfWork.Repository<Slot>();
    public async Task<MessageResponse> Handle(UpdateLocationPackageCommand request, CancellationToken cancellationToken)
    {
        if (!await _slotRepository.ExistsByAsync(_ => _.Id == request.NewSlotId, cancellationToken))
        {
            throw new NotFoundException(nameof(Slot), request.NewSlotId);
        }

        var package = await _packageRepository
            .FindByAsync(x => x.Id == request.Id && x.SlotId == request.CurrentSlotId, cancellationToken: cancellationToken);

        if (package is null)
        {
            throw new NotFoundException(nameof(Package), request.Id);
        }

        if (request.CurrentSlotId == request.NewSlotId)
        {
            return new MessageResponse(Resource.UpdatedSuccess);
        }

        if (!request.IsForce)
        {
            var slot = await _slotRepository
                .FindByAsync(_ =>
                    _.Id == request.NewSlotId &&
                    package.Volume <= _.Volume &&
                    package.Volume <= (_.Volume - _.Packages.Where(_ => _.Status != PackageStatus.Completed && _.Status != PackageStatus.Returned).Sum(_ => _.Volume)) &&
                    _.IsActive == true,
                 cancellationToken: cancellationToken);

            if (slot == null)
            {
                throw new NotFoundException(nameof(Slot), "slot are full");
            }

            request.NewSlotId = slot.Id;
        }

        package.SlotId = request.NewSlotId;
        await unitOfWork.CommitAsync(cancellationToken);

        return new MessageResponse(Resource.UpdatedSuccess);
    }
}
