using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShipperStation.Application.Common.Constants;
using ShipperStation.Application.Features.Shelfs.Commands;
using ShipperStation.Application.Features.Shelfs.Models;
using ShipperStation.Application.Features.Shelfs.Queries;
using ShipperStation.Application.Models;

namespace ShipperStation.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = Policies.StationManager)]
public class ShelfsController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetShelfs(int zoneId, CancellationToken cancellationToken)
    {
        return Ok(await sender.Send(new GetShelfsQuery() with { ZoneId = zoneId }, cancellationToken));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ShelfResponse>> GetShelfById(int id, CancellationToken cancellationToken)
    {
        return await sender.Send(new GetShelfByIdQuery(id), cancellationToken);
    }

    [HttpPost]
    public async Task<ActionResult<MessageResponse>> CreateShelf(CreateShelfCommand command, CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<MessageResponse>> UpdateShelf(int id, UpdateShelfCommand command, CancellationToken cancellationToken)
    {
        return await sender.Send(command with { Id = id }, cancellationToken);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<MessageResponse>> DeleteShelf(int id, CancellationToken cancellationToken)
    {
        return await sender.Send(new DeleteShelfCommand(id), cancellationToken);
    }
}
