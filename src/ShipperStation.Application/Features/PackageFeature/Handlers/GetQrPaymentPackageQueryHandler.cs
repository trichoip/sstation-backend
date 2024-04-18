using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.PackageFeature.Models;
using ShipperStation.Application.Features.PackageFeature.Queries;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.PackageFeature.Handlers;
internal sealed class GetQrPaymentPackageQueryHandler(
    IUnitOfWork unitOfWork,
    ICacheService cacheService,
    ICurrentUserService currentUserService) : IRequestHandler<GetQrPaymentPackageQuery, QrPaymentPackage>
{
    private readonly IGenericRepository<Package> _packageRepository = unitOfWork.Repository<Package>();
    public async Task<QrPaymentPackage> Handle(GetQrPaymentPackageQuery request, CancellationToken cancellationToken)
    {
        var package = await _packageRepository
            .FindByAsync<QrPaymentPackage>(_ => _.Id == request.Id,
            cancellationToken);

        if (package is null)
        {
            throw new NotFoundException(nameof(Package), request.Id);
        }

        await cacheService.SetWithExpirationAsync(package.Id.ToString(), new InfoStaffGennerateQrPaymentModel()
        {
            PackageId = package.Id,
            StaffId = await currentUserService.FindCurrentUserIdAsync(),
        }, TimeSpan.FromMinutes(5), cancellationToken);

        return package;
    }
}
