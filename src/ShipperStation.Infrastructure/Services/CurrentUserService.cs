using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ShipperStation.Application.Interfaces.Services;
using ShipperStation.Domain.Entities;
using System.Security.Claims;

namespace ShipperStation.Infrastructure.Services;
public class CurrentUserService(
    //UserManager<User> userManager,
    IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    private const string Anonymous = nameof(Anonymous);

    public string? CurrentUserId => CurrentUserPrincipal?.Identity?.Name ?? Anonymous;

    public ClaimsPrincipal? CurrentUserPrincipal => httpContextAccessor.HttpContext?.User;

    //public async Task<User?> FindCurrentUserAsync()
    //{
    //    if (CurrentUserPrincipal is null) return null;
    //    return await userManager.GetUserAsync(CurrentUserPrincipal);
    //}
}
