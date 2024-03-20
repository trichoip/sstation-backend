using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShipperStation.Application.Common.Constants;
using ShipperStation.Application.Features.Pricings.Commands;
using ShipperStation.Application.Features.Pricings.Models;
using ShipperStation.Application.Features.Pricings.Queries;
using ShipperStation.Application.Features.Staffs.Commands;
using ShipperStation.Application.Features.Staffs.Queries;
using ShipperStation.Application.Features.Stations.Models;
using ShipperStation.Application.Features.Stations.Queries;
using ShipperStation.Application.Features.Users.Models;
using ShipperStation.Application.Features.Zones.Commands;
using ShipperStation.Application.Features.Zones.Models;
using ShipperStation.Application.Features.Zones.Queries;
using ShipperStation.Application.Models;
using ShipperStation.Shared.Pages;

namespace ShipperStation.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class StationsController(ISender sender) : ControllerBase
{

    #region Staff
    [Authorize(Roles = Policies.StationManager)]
    [HttpPost("{stationId}/staffs")]
    public async Task<ActionResult<MessageResponse>> CreateStaff(
       int stationId,
       CreateStaffCommand request,
       CancellationToken cancellationToken)
    {
        return await sender.Send(request with { StationId = stationId }, cancellationToken);
    }

    [Authorize(Roles = Policies.StationManager)]
    [HttpGet("{stationId}/staffs")]
    public async Task<ActionResult<PaginatedResponse<UserResponse>>> GetStaffs(
        int stationId,
        [FromQuery] GetStaffsQuery request,
        CancellationToken cancellationToken)
    {
        return await sender.Send(request with { StationId = stationId }, cancellationToken);
    }

    [Authorize(Roles = Policies.StationManager)]
    [HttpGet("{stationId}/staffs/{staffId}")]
    public async Task<ActionResult<UserResponse>> GetStaffById(
        int stationId,
        Guid staffId,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new GetStaffByIdQuery(staffId) with { StationId = stationId }, cancellationToken);
    }

    [Authorize(Roles = Policies.StationManager)]
    [HttpDelete("{stationId}/staffs/{staffId}")]
    public async Task<ActionResult<MessageResponse>> DeleteStaff(
        int stationId,
        Guid staffId,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new DeleteStaffCommand(staffId) with { StationId = stationId }, cancellationToken);
    }

    [Authorize(Roles = Policies.StationManager)]
    [HttpPut("{stationId}/staffs/{staffId}")]
    public async Task<ActionResult<MessageResponse>> UpdateStaff(
        int stationId,
        Guid staffId,
        UpdateStaffCommand request,
        CancellationToken cancellationToken)
    {
        return await sender.Send(request with { StationId = stationId, StaffId = staffId }, cancellationToken);
    }
    #endregion

    #region Station

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<PaginatedResponse<StationResponse>>> GetAllStations(
        [FromQuery] GetAllStationsQuery request,
        CancellationToken cancellationToken)
    {
        return await sender.Send(request, cancellationToken);
    }
    #endregion

    #region Zones

    [Authorize(Roles = Policies.StationManager_Or_Staff)]
    [HttpGet("{stationId}/zones")]
    public async Task<IActionResult> GetZones(
        int stationId,
        [FromQuery] GetZonesQuery query,
        CancellationToken cancellationToken)
    {
        return Ok(await sender.Send(query with { StationId = stationId }, cancellationToken));
    }

    [Authorize(Roles = Policies.StationManager_Or_Staff)]
    [HttpGet("{stationId}/zones/{zoneId}")]
    public async Task<ActionResult<ZoneResponse>> GetZoneById(
        int stationId,
        int zoneId,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new GetZoneByIdQuery(zoneId) with { StationId = stationId }, cancellationToken);
    }

    [Authorize(Roles = Policies.StationManager)]
    [HttpPost("{stationId}/zones")]
    public async Task<ActionResult<MessageResponse>> CreateZone(
        int stationId,
        CreateZoneCommand command,
        CancellationToken cancellationToken)
    {
        return await sender.Send(command with { StationId = stationId }, cancellationToken);
    }

    [Authorize(Roles = Policies.StationManager)]
    [HttpPut("{stationId}/zones/{zoneId}")]
    public async Task<ActionResult<MessageResponse>> UpdateZone(
        int stationId,
        int zoneId,
        UpdateZoneCommand command,
        CancellationToken cancellationToken)
    {
        return await sender.Send(command with { Id = zoneId, StationId = stationId }, cancellationToken);
    }

    [Authorize(Roles = Policies.StationManager)]
    [HttpDelete("{stationId}/zones/{zoneId}")]
    public async Task<ActionResult<MessageResponse>> DeleteZone(
        int stationId,
        int zoneId,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new DeleteZoneCommand(zoneId) with { StationId = stationId }, cancellationToken);
    }

    #endregion

    #region Pricing

    [Authorize(Roles = Policies.StationManager_Or_Staff)]
    [HttpGet("{stationId}/pricings")]
    public async Task<IActionResult> GetPricings(
        int stationId,
        CancellationToken cancellationToken)
    {
        return Ok(await sender.Send(new GetPricingsQuery() with { StationId = stationId }, cancellationToken));
    }

    [Authorize(Roles = Policies.StationManager_Or_Staff)]
    [HttpGet("{stationId}/pricings/{pricingId}")]
    public async Task<ActionResult<PricingResponse>> GetPricingById(
        int stationId,
        int pricingId,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new GetPricingByIdQuery(pricingId) with { StationId = stationId }, cancellationToken);
    }

    [Authorize(Roles = Policies.StationManager)]
    [HttpPost("{stationId}/pricings")]
    public async Task<ActionResult<MessageResponse>> CreatePricing(
        int stationId,
        CreatePricingCommand command,
        CancellationToken cancellationToken)
    {
        return await sender.Send(command with { StationId = stationId }, cancellationToken);
    }

    [Authorize(Roles = Policies.StationManager)]
    [HttpPut("{stationId}/pricings/{pricingId}")]
    public async Task<ActionResult<MessageResponse>> UpdatePricing(
        int stationId,
        int pricingId,
        UpdatePricingCommand command,
        CancellationToken cancellationToken)
    {
        return await sender.Send(command with { Id = pricingId, StationId = stationId }, cancellationToken);
    }

    [Authorize(Roles = Policies.StationManager)]
    [HttpDelete("{stationId}/pricings/{pricingId}")]
    public async Task<ActionResult<MessageResponse>> DeletePricing(
        int stationId,
        int pricingId,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new DeletePricingCommand(pricingId) with { StationId = stationId }, cancellationToken);
    }

    #endregion

}
