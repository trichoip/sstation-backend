using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.PackageFeature.Commands;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Enums;

namespace ShipperStation.Infrastructure.Services;
public class PackageService(
    IUnitOfWork unitOfWork,
    ISender sender) : IPackageService
{

    private readonly IGenericRepository<Package> _packageRepository = unitOfWork.Repository<Package>();

    public async Task ExpirePackage(Guid id, CancellationToken cancellationToken = default)
    {
        var package = await _packageRepository.FindByAsync(_ => _.Id == id, cancellationToken: cancellationToken);

        if (package == null)
        {
            throw new NotFoundException(nameof(Package), id);
        }

        if (package.Status == PackageStatus.Initialized)
        {
            package.Status = PackageStatus.Expired;
            await unitOfWork.CommitAsync(cancellationToken);
        }

    }

    public async Task PushNotifyPackage()
    {
        var packages = await _packageRepository.FindAsync(_ => _.Status == PackageStatus.Initialized);

        var ids = packages.Select(_ => _.Id).ToList();

        await sender.Send(new PushNoticationReceivePackageCommand() with { Ids = ids });
    }
}
