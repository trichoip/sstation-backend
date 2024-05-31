namespace ShipperStation.Application.Contracts.Services;
public interface IPackageService
{
    Task ExpirePackage(Guid id, CancellationToken cancellationToken = default);
    Task PushNotifyPackage();
}
