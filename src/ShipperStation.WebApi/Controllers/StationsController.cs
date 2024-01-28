using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShipperStation.Application.Common.Constants;
using ShipperStation.Application.Features.Staffs.Commands;
using ShipperStation.Application.Features.Staffs.Queries;
using ShipperStation.Application.Features.Stations.Commands;
using ShipperStation.Application.Features.Users.Models;
using ShipperStation.Application.Models;
using ShipperStation.Shared.Pages;

namespace ShipperStation.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = Policies.StationManager)]
public class StationsController(ISender sender) : ControllerBase
{

    #region Staff
    [HttpPost("{stationId}/staff")]
    public async Task<ActionResult<MessageResponse>> CreateStaff(
       int stationId,
       CreateStaffCommand request,
       CancellationToken cancellationToken)
    {
        request = request with
        {
            StationId = stationId
        };

        return await sender.Send(request, cancellationToken);
    }

    [HttpGet("{stationId}/staff")]
    public async Task<ActionResult<PaginatedResponse<UserResponse>>> GetStaffs(
        int stationId,
        [FromQuery] GetStaffsQuery request,
        CancellationToken cancellationToken)
    {
        request = request with
        {
            StationId = stationId
        };

        return await sender.Send(request, cancellationToken);
    }
    #endregion

    [HttpPost]
    public async Task<ActionResult<MessageResponse>> CreateStation(
        CreateStationCommand command,
        CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

    #region Zones

    //[HttpGet("{stationId}/zones")]
    //public async Task<IActionResult> GetZones(CancellationToken cancellationToken)
    //{
    //    return Ok(await sender.Send(new GetZonesQuery(), cancellationToken));
    //}

    //[HttpGet("{stationId}/zones/{zoneId}")]
    //public async Task<ActionResult<ZoneResponse>> GetZoneById(int id, CancellationToken cancellationToken)
    //{
    //    return await sender.Send(new GetZoneByIdQuery(id), cancellationToken);
    //}

    //[HttpPost("{stationId}/zones")]
    //public async Task<ActionResult<MessageResponse>> CreateZone(CreateZoneCommand command, CancellationToken cancellationToken)
    //{
    //    return await sender.Send(command, cancellationToken);
    //}

    //[HttpPut("{stationId}/zones/{zoneId}")]
    //public async Task<ActionResult<MessageResponse>> UpdateZone(int id, UpdateZoneCommand command, CancellationToken cancellationToken)
    //{
    //    return await sender.Send(command with { Id = id }, cancellationToken);
    //}

    //[HttpDelete("{stationId}/zones/{zoneId}")]
    //public async Task<ActionResult<MessageResponse>> DeleteZone(int id, CancellationToken cancellationToken)
    //{
    //    return await sender.Send(new DeleteZoneCommand(id), cancellationToken);
    //}

    #endregion

}
