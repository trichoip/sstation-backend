using System.Security.Claims;

namespace ShipperStation.Application.Interfaces.Services;

public interface ICurrentPrincipalService
{
    public string? CurrentPrincipal { get; }

    public long? CurrentSubjectId { get; }

    public ClaimsPrincipal GetCurrentPrincipalFromToken(string token);

}