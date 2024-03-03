using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShipperStation.Application.Common.Constants;
using ShipperStation.Application.Features.Users.Commands;
using ShipperStation.Application.Features.Users.Models;
using ShipperStation.Application.Features.Users.Queries;
using ShipperStation.Application.Models;
using ShipperStation.Shared.Pages;

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

    [Authorize(Roles = Policies.StationManager_Or_Staff)]
    [HttpGet]
    public async Task<ActionResult<PaginatedResponse<UserResponse>>> GetUsers([FromQuery] GetUsersQuery query, CancellationToken cancellationToken)
    {
        return await sender.Send(query, cancellationToken);
    }

    [Authorize(Roles = Policies.StationManager_Or_Staff)]
    [HttpGet("phone/{number}")]
    public async Task<ActionResult<UserResponse>> GetUserByPhone(string number, CancellationToken cancellationToken)
    {
        return await sender.Send(new GetUserByPhoneQuery(number), cancellationToken);
    }

    [Authorize(Roles = Policies.StationManager_Or_Staff)]
    [HttpPost]
    public async Task<ActionResult<MessageResponse>> CreateUser(CreateUserCommand command, CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }
}
