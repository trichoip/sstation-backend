using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShipperStation.Application.Common.Constants;
using ShipperStation.Application.Features.Stations.Commands;
using ShipperStation.Application.Features.Stations.Models;
using ShipperStation.Application.Features.Stations.Queries;
using ShipperStation.Application.Models;
using ShipperStation.Shared.Pages;

namespace ShipperStation.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AdminController(ISender sender) : ControllerBase
{
    [Authorize(Roles = Policies.Admin)]
    [HttpPost("stations")]
    public async Task<ActionResult<MessageResponse>> CreateStation(
    CreateStationCommand command,
    CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

    [Authorize(Roles = Policies.Admin)]
    [HttpGet("stations")]
    public async Task<ActionResult<PaginatedResponse<StationResponse>>> GetAllStations(
    [FromQuery] GetAllStationsQuery request,
    CancellationToken cancellationToken)
    {
        return await sender.Send(request, cancellationToken);
    }
}
