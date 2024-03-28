using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.PackageFeature.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.PackageFeature.Handlers;
internal sealed class ConfirmPackageCommandHandler(
    IUnitOfWork unitOfWork,
    IPublisher publisher) : IRequestHandler<ConfirmPackageCommand, MessageResponse>
{
    private readonly IGenericRepository<Package> _packageRepository = unitOfWork.Repository<Package>();
    public async Task<MessageResponse> Handle(ConfirmPackageCommand request, CancellationToken cancellationToken)
    {
        var package = await _packageRepository
            .FindByAsync(_ => _.Id == request.Id, cancellationToken: cancellationToken);

        if (package == null)
        {
            throw new NotFoundException(nameof(Package), request.Id);
        }

        //if (package.Status != PackageStatus.Paid)
        //{
        //    throw new BadRequestException("Package is not paid");
        //}

        return new MessageResponse("Confirm Success");
    }
}
