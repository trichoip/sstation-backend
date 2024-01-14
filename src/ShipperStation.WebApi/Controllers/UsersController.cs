using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShipperStation.Application.Contracts;
using ShipperStation.Application.Contracts.Users;
using ShipperStation.Application.Features.Users.Commands.SetPassword;
using ShipperStation.Application.Features.Users.Commands.UpdateProfile;
using ShipperStation.Application.Features.Users.Queries.GetProfile;

namespace ShipperStation.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UsersController(ISender sender) : ControllerBase
{
    [HttpPatch("set-password")]
    public async Task<ActionResult<MessageResponse>> SetPassword(SetPasswordCommand command, CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

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
}
