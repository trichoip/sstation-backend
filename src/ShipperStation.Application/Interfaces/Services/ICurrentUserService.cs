using System.Security.Claims;

namespace ShipperStation.Application.Interfaces.Services;
public interface ICurrentUserService
{
    public string? CurrentUserId { get; }

    public ClaimsPrincipal? CurrentUserPrincipal { get; }

    //Task<User?> FindCurrentUserAsync();
}
