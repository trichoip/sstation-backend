using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShipperStation.Application.Features.Auth.Commands;
using ShipperStation.Application.Features.Auth.Models;
using ShipperStation.Application.Models;

namespace ShipperStation.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
//[SwaggerTag("Api for auth")]
public class AuthController(ISender sender) : ControllerBase
{

    /// <summary>
    /// Login for admin, staff, store manager
    /// </summary>
    /// <remarks>
    /// ```
    /// Admin account: admin - admin
    /// Staff account: staff - staff
    /// Store manager account: store - store
    /// ```
    /// </remarks>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<ActionResult<AccessTokenResponse>> Login(LoginRequest request, CancellationToken cancellationToken)
    {
        return await sender.Send(request, cancellationToken);
    }

    /// <summary>
    /// Send otp to user's phone number
    /// </summary>
    /// <param name="request">
    /// ```
    /// The beginning of the phone number can be (0 or +84 or 84)
    /// 
    /// Regex phone number: ^(\+84|84|0)[35789][0-9]{8}$
    /// ```
    /// </param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("send-otp")]
    public async Task<ActionResult<MessageResponse>> SendOtp(SendOtpRequest request, CancellationToken cancellationToken)
    {
        return await sender.Send(request, cancellationToken);
    }

    [HttpPost("verify-otp")]
    public async Task<ActionResult<AccessTokenResponse>> VerifyOtp(VerifyOtpRequest request, CancellationToken cancellationToken)
    {
        return await sender.Send(request, cancellationToken);
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<AccessTokenResponse>> RefreshToken(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        return await sender.Send(request, cancellationToken);
    }

}
