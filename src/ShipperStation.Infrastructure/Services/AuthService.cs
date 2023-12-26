
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using ShipperStation.Application.Common.Constants;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Contracts.Auth;
using ShipperStation.Application.Interfaces.Repositories;
using ShipperStation.Application.Interfaces.Services;
using ShipperStation.Domain.Constants;
using ShipperStation.Domain.Entities.Identities;
using ShipperStation.Shared.Extensions;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ShipperStation.Infrastructure.Services;
public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;

    public AuthService(
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        IUnitOfWork unitOfWork,
        IJwtService jwtService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
    }

    public async Task<AccessTokenResponse> SignInExternalAsync(ExternalAuthRequest externalAuthRequest, CancellationToken cancellationToken = default)
    {
        var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(externalAuthRequest.IdToken);
        var subject = jwtSecurityToken.Subject.OrElseThrow(() => new UnauthorizedAccessException("Subject in payload is null"));

        var claims = jwtSecurityToken.Claims;
        var email = claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email)?.Value;
        var name = claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Name)?.Value;
        var picture = claims.FirstOrDefault(x => x.Type == "picture")?.Value;

        if (await _unitOfWork.Repository<User>().FindByAsync(x => x.UserName == subject, cancellationToken: cancellationToken) is not { } user)
        {
            user = new User()
            {

                Email = email,
                FullName = name,

            };

            await _unitOfWork.Repository<User>().CreateAsync(user, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
        }
        return await _jwtService.GenerateTokenAsync(user);
    }

    public async Task<AccessTokenResponse> SignInAsync(LoginRequest loginRequest, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByNameAsync(loginRequest.Username);
        if (user == null)
        {
            throw new UnauthorizedAccessException("Login failed.");
        }
        if (!await _userManager.CheckPasswordAsync(user, loginRequest.Password))
        {
            throw new UnauthorizedAccessException("Login failed.");
        }
        return await _jwtService.GenerateTokenAsync(user);
    }

    public async Task<AccessTokenResponse> RefreshTokenAsync(RefreshRequest refreshRequest, CancellationToken cancellationToken = default)
    {
        var user = await _jwtService.ValidateRefreshTokenAsync(refreshRequest.RefreshToken);
        if (user == null)
        {
            throw new UnauthorizedAccessException("Refresh token is not valid.");
        }
        return await _jwtService.GenerateTokenAsync(user);
    }

    public async Task<(string userId, string code)> RegisterAsync(RegisterRequest registerRequest, CancellationToken cancellationToken = default)
    {
        var user = new User
        {
            UserName = registerRequest.Username,
            Email = registerRequest.Email
        };

        var result = await _userManager.CreateAsync(user, registerRequest.Password);
        if (!result.Succeeded)
        {
            throw new ValidationBadRequestException(result.Errors);
        }

        if (!await _roleManager.RoleExistsAsync(Roles.User))
        {
            result = await _roleManager.CreateAsync(new Role { Name = Roles.User });
            if (!result.Succeeded)
            {
                throw new ValidationBadRequestException(result.Errors);
            }
        }
        result = await _userManager.AddToRoleAsync(user, Roles.User);
        if (!result.Succeeded)
        {
            throw new ValidationBadRequestException(result.Errors);
        }

        var userId = await _userManager.GetUserIdAsync(user);
        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        return (userId, code);
    }

    public async Task<bool> ConfirmEmailAsync(string userId, string code, CancellationToken cancellationToken = default)
    {
        if (await _userManager.FindByIdAsync(userId) is not { } user) return false;
        //throw new UnauthorizedAccessException("Unauthorized");

        code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));

        var result = await _userManager.ConfirmEmailAsync(user, code);

        //if (!result.Succeeded)
        //{
        //    //throw new ValidationBadRequestException(result.Errors);
        //}

        return result.Succeeded;
    }

    public async Task<string> ForgotPasswordAsync(ForgotPasswordRequest forgotPasswordRequest, CancellationToken cancellationToken = default)
    {
        if (await _userManager.FindByEmailAsync(forgotPasswordRequest.Email) is not { } user ||
            !await _userManager.IsEmailConfirmedAsync(user))
            throw new UnauthorizedAccessException("email not found or your email does not confirmed ");

        var code = await _userManager.GenerateUserTokenAsync(user, TokenOptions.DefaultEmailProvider, Token.ResetPassword);

        return code;
    }

    public async Task ResetPasswordAsync(ResetPasswordRequest resetPasswordRequest, CancellationToken cancellationToken = default)
    {
        if (await _userManager.FindByEmailAsync(resetPasswordRequest.Email) is not { } user ||
            !await _userManager.IsEmailConfirmedAsync(user))
        {
            throw new UnauthorizedAccessException("email not found or your email does not confirmed ");
        }

        if (!await _userManager.VerifyUserTokenAsync(user, TokenOptions.DefaultEmailProvider, Token.ResetPassword, resetPasswordRequest.ResetCode))
        {
            throw new UnauthorizedAccessException("Invalid Token");
        }

        var result = await _userManager.RemovePasswordAsync(user);
        if (!result.Succeeded)
        {
            throw new ValidationBadRequestException(result.Errors);
        }

        result = await _userManager.AddPasswordAsync(user, resetPasswordRequest.NewPassword);
        if (!result.Succeeded)
        {
            throw new ValidationBadRequestException(result.Errors);
        }

    }

    public async Task<(string userId, string code)> ResendEmailConfirmationAsync(ResendEmailRequest resendEmailRequest, CancellationToken cancellationToken = default)
    {
        if (await _userManager.FindByEmailAsync(resendEmailRequest.Email) is not { } user)
            throw new UnauthorizedAccessException("email not found");

        var userId = await _userManager.GetUserIdAsync(user);
        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        return (userId, code);
    }
}

