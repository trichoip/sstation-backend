using ShipperStation.Domain.Entities.Identities;
using System.Security.Claims;

namespace ShipperStation.Application.Contracts.Services;
public interface ICurrentUserService
{
    public string? CurrentUserId { get; }

    public ClaimsPrincipal? CurrentUserPrincipal { get; }

    public string ServerUrl { get; }

    Task<User> FindCurrentUserAsync();
    Task<Guid> FindCurrentUserIdAsync();
}
