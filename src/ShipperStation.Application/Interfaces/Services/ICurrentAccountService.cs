using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Interfaces.Services;

public interface ICurrentAccountService
{
    public Task<User?> GetCurrentAccount();

    public Task<User> GetRequiredCurrentAccount();
}