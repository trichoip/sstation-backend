using ShipperStation.Application.DTOs.Auth;

namespace ShipperStation.Application.Interfaces.Services;
public interface IAuthService
{
    Task<AccessTokenResponse> SignInExternalAsync(ExternalAuthRequest externalAuthRequest);

    Task<AccessTokenResponse> SignInAsync(LoginRequest loginRequest);
    Task<AccessTokenResponse> RefreshTokenAsync(RefreshRequest refreshRequest);
    Task<(string userId, string code)> RegisterAsync(RegisterRequest registerRequest);
    Task<bool> ConfirmEmailAsync(string userId, string code);
    Task<string> ForgotPasswordAsync(ForgotPasswordRequest forgotPasswordRequest);
    Task ResetPasswordAsync(ResetPasswordRequest resetPasswordRequest);
    Task<(string userId, string code)> ResendEmailConfirmationAsync(ResendEmailRequest resendEmailRequest);
}
