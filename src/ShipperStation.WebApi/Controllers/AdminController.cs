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
[Authorize(Roles = Policies.Admin)]
public class AdminController(ISender sender) : ControllerBase
{
    [HttpPost("stations")]
    public async Task<ActionResult<MessageResponse>> CreateStation(
        CreateStationCommand command,
        CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

    [HttpGet("stations")]
    public async Task<ActionResult<PaginatedResponse<StationResponse>>> GetAllStations(
        [FromQuery] GetAllStationsQuery request,
        CancellationToken cancellationToken)
    {
        return await sender.Send(request, cancellationToken);
    }

    [HttpGet("stations/{id}")]
    public async Task<ActionResult<StationResponse>> GetStationByIdForAdmin(
        int id,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new GetStationByIdForAdminQuery(id), cancellationToken);
    }

    [HttpDelete("stations/{id}")]
    public async Task<ActionResult<MessageResponse>> DeleteStationByAdmin(
        int id,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new DeleteStationByAdminCommand(id), cancellationToken);
    }

    [HttpPut("stations/{id}")]
    public async Task<ActionResult<MessageResponse>> UpdateStationByAdmin(
        int id,
        UpdateStationByAdminCommand request,
        CancellationToken cancellationToken)
    {
        return await sender.Send(request with { Id = id }, cancellationToken);
    }

}
