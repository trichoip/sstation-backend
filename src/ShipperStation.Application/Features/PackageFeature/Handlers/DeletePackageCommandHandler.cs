using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.PackageFeature.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.PackageFeature.Handlers;
internal sealed class DeletePackageCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeletePackageCommand, MessageResponse>
{

    private readonly IGenericRepository<Package> _packageRepository = unitOfWork.Repository<Package>();
    public async Task<MessageResponse> Handle(DeletePackageCommand request, CancellationToken cancellationToken)
    {
        var package = await _packageRepository.FindByAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (package is null)
        {
            throw new NotFoundException(nameof(Package), request.Id);
        }

        await _packageRepository.DeleteAsync(package);
        await unitOfWork.CommitAsync(cancellationToken);

        return new MessageResponse(Resource.DeletedSuccess);
    }
}
