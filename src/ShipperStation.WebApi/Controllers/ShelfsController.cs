using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShipperStation.Application.Common.Constants;
using ShipperStation.Application.Features.Shelfs.Commands;
using ShipperStation.Application.Features.Shelfs.Models;
using ShipperStation.Application.Features.Shelfs.Queries;
using ShipperStation.Application.Models;
using ShipperStation.Shared.Pages;

namespace ShipperStation.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]

public class ShelfsController(ISender sender) : ControllerBase
{
    [Authorize(Roles = Policies.StationManager_Or_Staff_Or_Admin)]
    [HttpGet]
    public async Task<ActionResult<PaginatedResponse<ShelfResponse>>> GetShelfs(
        [FromQuery] GetShelfsQuery query,
        int zoneId,
        CancellationToken cancellationToken)
    {
        return await sender.Send(query with { ZoneId = zoneId }, cancellationToken);
    }

    [Authorize(Roles = Policies.StationManager_Or_Staff_Or_Admin)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ShelfResponse>> GetShelfById(int id, CancellationToken cancellationToken)
    {
        return await sender.Send(new GetShelfByIdQuery(id), cancellationToken);
    }

    [Authorize(Roles = Policies.Admin_Or_StationManager)]
    [HttpPost]
    public async Task<ActionResult<MessageResponse>> CreateShelf(CreateShelfCommand command, CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

    [Authorize(Roles = Policies.Admin_Or_StationManager)]
    [HttpPut("{id}")]
    public async Task<ActionResult<MessageResponse>> UpdateShelf(int id, UpdateShelfCommand command, CancellationToken cancellationToken)
    {
        return await sender.Send(command with { Id = id }, cancellationToken);
    }

    [Authorize(Roles = Policies.Admin_Or_StationManager)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<MessageResponse>> DeleteShelf(int id, CancellationToken cancellationToken)
    {
        return await sender.Send(new DeleteShelfCommand(id), cancellationToken);
    }
}
