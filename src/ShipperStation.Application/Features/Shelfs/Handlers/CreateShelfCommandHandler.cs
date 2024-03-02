using MediatR;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.Shelfs.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Shelfs.Handlers;
internal sealed class CreateShelfCommandHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService) : IRequestHandler<CreateShelfCommand, MessageResponse>
{
    private readonly IGenericRepository<Rack> _rackRepository = unitOfWork.Repository<Rack>();
    //private readonly IGenericRepository<Size> _sizeRepository = unitOfWork.Repository<Size>();
    private readonly IGenericRepository<Zone> _zoneRepository = unitOfWork.Repository<Zone>();
    public async Task<MessageResponse> Handle(CreateShelfCommand request, CancellationToken cancellationToken)
    {

        var userId = await currentUserService.FindCurrentUserIdAsync();

        //if (!await _sizeRepository
        //    .ExistsByAsync(_ => _.Id == request.SizeId, cancellationToken))
        //{
        //    throw new NotFoundException(nameof(Size), request.SizeId);
        //}

        //if (!await _zoneRepository
        //    .ExistsByAsync(_ =>
        //        _.Id == request.ZoneId &&
        //        _.Station.UserStations.Any(_ => _.UserId == userId),
        //    cancellationToken))
        //{
        //    throw new NotFoundException(nameof(Zone), request.ZoneId);
        //}

        //// create shelf and slot

        //var rack = request.Adapt<Rack>();
        //await _rackRepository.CreateAsync(rack, cancellationToken);
        //await unitOfWork.CommitAsync(cancellationToken);

        return new MessageResponse(Resource.CreatedSuccess);
    }
}
