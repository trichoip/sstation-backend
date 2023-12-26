using ShipperStation.Application.Contracts.Auth;

namespace ShipperStation.Application.Interfaces.Services;
public interface IAuthService
{
    Task<AccessTokenResponse> SignInExternalAsync(ExternalAuthRequest externalAuthRequest, CancellationToken cancellationToken = default);

    Task<AccessTokenResponse> SignInAsync(LoginRequest loginRequest, CancellationToken cancellationToken = default);
    Task<AccessTokenResponse> RefreshTokenAsync(RefreshRequest refreshRequest, CancellationToken cancellationToken = default);
    Task<(string userId, string code)> RegisterAsync(RegisterRequest registerRequest, CancellationToken cancellationToken = default);
    Task<bool> ConfirmEmailAsync(string userId, string code, CancellationToken cancellationToken = default);
    Task<string> ForgotPasswordAsync(ForgotPasswordRequest forgotPasswordRequest, CancellationToken cancellationToken = default);
    Task ResetPasswordAsync(ResetPasswordRequest resetPasswordRequest, CancellationToken cancellationToken = default);
    Task<(string userId, string code)> ResendEmailConfirmationAsync(ResendEmailRequest resendEmailRequest, CancellationToken cancellationToken = default);
}
