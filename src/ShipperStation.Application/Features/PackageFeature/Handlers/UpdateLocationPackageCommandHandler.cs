using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.PackageFeature.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.PackageFeature.Handlers;
internal sealed class UpdateLocationPackageCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateLocationPackageCommand, MessageResponse>
{
    private readonly IGenericRepository<Package> _packageRepository = unitOfWork.Repository<Package>();
    private readonly IGenericRepository<Rack> _rackRepository = unitOfWork.Repository<Rack>();
    public async Task<MessageResponse> Handle(UpdateLocationPackageCommand request, CancellationToken cancellationToken)
    {
        if (!await _rackRepository.ExistsByAsync(_ => _.Id == request.NewRackId, cancellationToken))
        {
            throw new NotFoundException(nameof(Rack), request.NewRackId);
        }

        var package = await _packageRepository
            .FindByAsync(x => x.Id == request.Id && x.RackId == request.CurrentRackId, cancellationToken: cancellationToken);

        if (package is null)
        {
            throw new NotFoundException(nameof(Package), request.Id);
        }

        if (request.CurrentRackId == request.NewRackId)
        {
            return new MessageResponse(Resource.UpdatedSuccess);
        }

        var rack = await _rackRepository
            .FindByAsync(_ => _.Id == request.NewRackId, cancellationToken: cancellationToken);

        if (rack == null)
        {
            throw new NotFoundException(nameof(rack), request.NewRackId);
        }

        package.RackId = rack.Id;
        await unitOfWork.CommitAsync(cancellationToken);

        return new MessageResponse(Resource.UpdatedSuccess);
    }
}
