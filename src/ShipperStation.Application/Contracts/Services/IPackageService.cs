namespace ShipperStation.Application.Contracts.Services;
public interface IPackageService
{
    Task CheckReceivePackageAsync(Guid packageId);
}
