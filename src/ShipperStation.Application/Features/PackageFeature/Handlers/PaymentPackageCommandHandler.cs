using MediatR;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.PackageFeature.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.PackageFeature.Handlers;
internal sealed class PaymentPackageCommandHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService) : IRequestHandler<PaymentPackageCommand, MessageResponse>
{
    private readonly IGenericRepository<Package> _packageRepository = unitOfWork.Repository<Package>();
    public async Task<MessageResponse> Handle(PaymentPackageCommand request, CancellationToken cancellationToken)
    {
        var package = await _packageRepository
            .FindByAsync(_ => _.Id == request.Id, cancellationToken: cancellationToken);

        return new MessageResponse("Payment Success");
    }
}
