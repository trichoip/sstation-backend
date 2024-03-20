using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShipperStation.Application.Common.Constants;
using ShipperStation.Application.Features.Stations.Models;
using ShipperStation.Application.Features.Stations.Queries;
using ShipperStation.Application.Features.Users.Commands;
using ShipperStation.Application.Features.Users.Models;
using ShipperStation.Application.Features.Users.Queries;
using ShipperStation.Application.Models;
using ShipperStation.Shared.Pages;

namespace ShipperStation.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class StaffsController(ISender sender) : ControllerBase
{
    [HttpGet("stations")]
    [Authorize(Roles = Policies.Staff)]
    public async Task<ActionResult<StationResponse>> GetStationByStaff(CancellationToken cancellationToken)
    {
        return await sender.Send(new GetStationByStaffQuery(), cancellationToken);
    }

    [Authorize(Roles = Policies.StationManager_Or_Staff)]
    [HttpGet("users")]
    public async Task<ActionResult<PaginatedResponse<UserResponse>>> GetUsers([FromQuery] GetUsersQuery query, CancellationToken cancellationToken)
    {
        return await sender.Send(query, cancellationToken);
    }

    [Authorize(Roles = Policies.StationManager_Or_Staff)]
    [HttpGet("users/phone/{number}")]
    public async Task<ActionResult<UserResponse>> GetUserByPhone(string number, CancellationToken cancellationToken)
    {
        return await sender.Send(new GetUserByPhoneQuery(number), cancellationToken);
    }

    [Authorize(Roles = Policies.StationManager_Or_Staff)]
    [HttpPost("users")]
    public async Task<ActionResult<MessageResponse>> CreateUser(CreateUserCommand command, CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

}
