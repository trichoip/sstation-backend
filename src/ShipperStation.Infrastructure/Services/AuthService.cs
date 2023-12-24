
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Constants;
using ShipperStation.Application.DTOs.Auth;
using ShipperStation.Application.Helpers;
using ShipperStation.Application.Interfaces.Repositories;
using ShipperStation.Application.Interfaces.Services;
using ShipperStation.Domain.Constants;
using ShipperStation.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ShipperStation.Infrastructure.Services;
public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly TokenHelper<User> _tokenHelper;

    public AuthService(
        UserManager<User> userManager,
        RoleManager<IdentityRole<int>> roleManager,
        IUnitOfWork unitOfWork,
        TokenHelper<User> tokenHelper)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _unitOfWork = unitOfWork;
        _tokenHelper = tokenHelper;
    }

    public async Task<AccessTokenResponse> SignInExternalAsync(ExternalAuthRequest externalAuthRequest)
    {
        var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(externalAuthRequest.IdToken);
        var subject = jwtSecurityToken.Subject.OrElseThrow(() => new UnauthorizedAccessException("Subject in payload is null"));

        var claims = jwtSecurityToken.Claims;
        var email = claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email)?.Value;
        var name = claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Name)?.Value;
        var picture = claims.FirstOrDefault(x => x.Type == "picture")?.Value;

        if (await _unitOfWork.Repository<User>().FindByAsync(x => x.UserName == subject) is not { } user)
        {
            user = new User()
            {

                Email = email,
                FullName = name,

            };

            await _unitOfWork.Repository<User>().CreateAsync(user);
            await _unitOfWork.CommitAsync();
        }
        return await _tokenHelper.CreateTokenAsync(user);
    }

    public async Task<AccessTokenResponse> SignInAsync(LoginRequest loginRequest)
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
        return await _tokenHelper.CreateTokenAsync(user: user);
    }

    public async Task<AccessTokenResponse> RefreshTokenAsync(RefreshRequest refreshRequest)
    {
        var user = await _tokenHelper.ValidateRefreshTokenAsync(refreshRequest.RefreshToken);
        if (user == null)
        {
            throw new UnauthorizedAccessException("Refresh token is not valid.");
        }
        return await _tokenHelper.CreateTokenAsync(user: user);
    }

    public async Task<(string userId, string code)> RegisterAsync(RegisterRequest registerRequest)
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
            result = await _roleManager.CreateAsync(new IdentityRole<int> { Name = Roles.User });
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

    public async Task<bool> ConfirmEmailAsync(string userId, string code)
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

    public async Task<string> ForgotPasswordAsync(ForgotPasswordRequest forgotPasswordRequest)
    {
        if (await _userManager.FindByEmailAsync(forgotPasswordRequest.Email) is not { } user ||
            !await _userManager.IsEmailConfirmedAsync(user))
            throw new UnauthorizedAccessException("email not found or your email does not confirmed ");

        var code = await _userManager.GenerateUserTokenAsync(user, TokenOptions.DefaultEmailProvider, Token.ResetPassword);

        return code;
    }

    public async Task ResetPasswordAsync(ResetPasswordRequest resetPasswordRequest)
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

    public async Task<(string userId, string code)> ResendEmailConfirmationAsync(ResendEmailRequest resendEmailRequest)
    {
        if (await _userManager.FindByEmailAsync(resendEmailRequest.Email) is not { } user)
            throw new UnauthorizedAccessException("email not found");

        var userId = await _userManager.GetUserIdAsync(user);
        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        return (userId, code);
    }
}

