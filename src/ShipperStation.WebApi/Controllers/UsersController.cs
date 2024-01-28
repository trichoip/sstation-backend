using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShipperStation.Application.Features.Users.Commands;
using ShipperStation.Application.Features.Users.Models;
using ShipperStation.Application.Features.Users.Queries;
using ShipperStation.Application.Models;

namespace ShipperStation.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UsersController(ISender sender) : ControllerBase
{

    [HttpGet("profile")]
    public async Task<ActionResult<UserResponse>> GetProfile(CancellationToken cancellationToken)
    {
        return await sender.Send(new GetProfileQuery(), cancellationToken);
    }

    [HttpPut("profile")]
    public async Task<ActionResult<MessageResponse>> UpdateProfile(UpdateProfileCommand command, CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

    [HttpPatch("set-password")]
    public async Task<ActionResult<MessageResponse>> SetPassword(SetPasswordCommand command, CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

    [HttpPost("change-password")]
    public async Task<ActionResult<MessageResponse>> ChangePassword(ChangePasswordRequest request, CancellationToken cancellationToken)
    {
        return await sender.Send(request, cancellationToken);
    }

    [HttpPost("reset-password")]
    public async Task<ActionResult<MessageResponse>> ResetPassword(ResetPasswordRequest request, CancellationToken cancellationToken)
    {
        return await sender.Send(request, cancellationToken);
    }
}
