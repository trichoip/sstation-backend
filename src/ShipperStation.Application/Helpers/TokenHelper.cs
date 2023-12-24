using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Constants;
using ShipperStation.Application.DTOs.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ShipperStation.Application.Helpers;

public class TokenHelper<T> where T : IdentityUser<Guid>
{
    private readonly SignInManager<T> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly IDataProtectionProvider _dataProtectionProvider;
    private readonly IDataProtector protector;
    private readonly TicketDataFormat ticketDataFormat;

    public TokenHelper(
        SignInManager<T> signInManager,
        IConfiguration configuration,
        IDataProtectionProvider dataProtectionProvider)
    {
        _signInManager = signInManager;
        _configuration = configuration;
        _dataProtectionProvider = dataProtectionProvider;

        protector = _dataProtectionProvider.CreateProtector(Token.RefreshToken);
        ticketDataFormat = new TicketDataFormat(protector);

    }

    public async Task<AccessTokenResponse> CreateTokenAsync(T? user = null)
    {

        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);

        var key = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("Authentication:Schemes:Bearer:SerectKey").Value!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var token = new JwtSecurityToken(
            claims: claimsPrincipal.Claims,
            expires: DateTime.UtcNow.AddHours(Token.ExpiresIn),
            signingCredentials: creds);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        var response = new AccessTokenResponse
        {
            AccessToken = jwt,
            ExpiresIn = (long)TimeSpan.FromHours(Token.ExpiresIn).TotalSeconds,
            RefreshToken = ticketDataFormat.Protect(CreateRefreshTicket(claimsPrincipal, DateTimeOffset.UtcNow)),
        };
        return response;
    }

    public async Task<T> ValidateRefreshTokenAsync(string refreshToken)
    {
        if (string.IsNullOrEmpty(refreshToken)) throw new BadRequestException("RefreshToken must be provided");

        var ticket = ticketDataFormat.Unprotect(refreshToken);

        if (ticket?.Properties?.ExpiresUtc is not { } expiresUtc ||
            DateTimeOffset.UtcNow >= expiresUtc ||
            await _signInManager.ValidateSecurityStampAsync(ticket.Principal) is not T user)
        {
            throw new UnauthorizedAccessException("Refresh token is not valid!");
        }
        return user;
    }

    private static AuthenticationTicket CreateRefreshTicket(ClaimsPrincipal user, DateTimeOffset utcNow)
    {
        var refreshProperties = new AuthenticationProperties
        {
            ExpiresUtc = utcNow.AddDays(14)
        };

        return new AuthenticationTicket(user, refreshProperties, Token.Bearer);
    }
}