using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Extensions;
using ShipperStation.Application.Enums;
using ShipperStation.Application.Interfaces.Services;
using ShipperStation.Infrastructure.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShipperStation.Infrastructure.Services;

public class CurrentPrincipalService : ICurrentPrincipalService
{
    private readonly IHttpContextAccessor _accessor;
    private readonly IConfiguration _configuration;
    private readonly JwtSettings _jwtSettings;

    public CurrentPrincipalService(IHttpContextAccessor accessor, IConfiguration configuration, JwtSettings jwtSettings)
    {
        _accessor = accessor;
        _configuration = configuration;
        _jwtSettings = jwtSettings;
    }

    // Get current login acc Id
    public string? CurrentPrincipal
    {
        get
        {
            var identity = _accessor?.HttpContext?.User.Identity as ClaimsIdentity;

            if (identity == null || !identity.IsAuthenticated) return null;

            var claims = identity.Claims;

            var id = claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value ?? null;

            return id;
        }
    }

    public long? CurrentSubjectId => CurrentPrincipal != null ? long.Parse(CurrentPrincipal) : null;

    public ClaimsPrincipal GetCurrentPrincipalFromToken(string token)
    {
        var tokenValidationParams = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _jwtSettings.Issuer,
            ValidAudience = _jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key))
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParams, out var securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
        {
            throw new ApplicationException(ResponseCode.AuthErrorInvalidRefreshToken.GetDisplayName());
        }

        return principal;
    }

}