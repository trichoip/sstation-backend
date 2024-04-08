using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShipperStation.Application.Common.Constants;
using ShipperStation.Application.Features.Racks.Commands;
using ShipperStation.Application.Features.Racks.Models;
using ShipperStation.Application.Features.Racks.Queries;
using ShipperStation.Application.Models;
using ShipperStation.Shared.Pages;

namespace ShipperStation.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RacksController(ISender sender) : ControllerBase
{
    [Authorize(Roles = Policies.StationManager_Or_Staff)]
    [HttpGet]
    public async Task<ActionResult<PaginatedResponse<RackResponse>>> GetRacks(
         [FromQuery] GetRacksQuery query,
         CancellationToken cancellationToken)
    {
        return await sender.Send(query, cancellationToken);
    }

    [Authorize(Roles = Policies.StationManager_Or_Staff)]
    [HttpGet("{id}")]
    public async Task<ActionResult<RackResponse>> GetRackById(int id, CancellationToken cancellationToken)
    {
        return await sender.Send(new GetRackByIdQuery(id), cancellationToken);
    }

    [Authorize(Roles = Policies.StationManager)]
    [HttpPost]
    public async Task<ActionResult<MessageResponse>> CreateRack(CreateRackCommand command, CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

    [Authorize(Roles = Policies.StationManager)]
    [HttpPut("{id}")]
    public async Task<ActionResult<MessageResponse>> UpdateRack(int id, UpdateRackCommand command, CancellationToken cancellationToken)
    {
        return await sender.Send(command with { Id = id }, cancellationToken);
    }

    [Authorize(Roles = Policies.StationManager)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<MessageResponse>> DeleteRack(int id, CancellationToken cancellationToken)
    {
        return await sender.Send(new DeleteRackCommand(id), cancellationToken);
    }
}
