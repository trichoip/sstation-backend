using Microsoft.Extensions.DependencyInjection;
using ShipperStation.Application.Enums;
using ShipperStation.Application.Interfaces.Repositories;
using ShipperStation.Application.Interfaces.Services;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Infrastructure.Services;

public class CurrentAccountService : ICurrentAccountService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ICurrentPrincipalService _currentPrincipalService;

    public CurrentAccountService(
        ICurrentPrincipalService currentPrincipalService,
        IServiceScopeFactory serviceScopeFactory)
    {
        _currentPrincipalService = currentPrincipalService;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task<User?> GetCurrentAccount()
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
        var currentAccountId = _currentPrincipalService.CurrentSubjectId;
        if (currentAccountId == null)
        {
            return null;
        }
        throw new ApplicationException(ResponseCode.Unauthorized.ToString());
        //return await unitOfWork.AccountRepository.GetByIdAsync(currentAccountId);
    }

    public async Task<User> GetRequiredCurrentAccount()
    {
        return await GetCurrentAccount() ?? throw new ApplicationException(ResponseCode.Unauthorized.ToString());
    }
}