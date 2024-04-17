using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShipperStation.Application.Common.Constants;
using ShipperStation.Application.Features.Slots.Commands;
using ShipperStation.Application.Features.Slots.Models;
using ShipperStation.Application.Features.Slots.Queries;
using ShipperStation.Application.Models;
using ShipperStation.Shared.Pages;

namespace ShipperStation.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = Policies.StationManager_Or_Staff_Or_Admin)]
public class SlotsController(ISender sender) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<PaginatedResponse<SlotResponse>>> GetSlots(
        [FromQuery] GetSlotsQuery query,
        CancellationToken cancellationToken)
    {
        return await sender.Send(query, cancellationToken);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SlotResponse>> GetSlotById(int id, CancellationToken cancellationToken)
    {
        return await sender.Send(new GetSlotByIdQuery(id), cancellationToken);
    }

    [HttpPost]
    public async Task<ActionResult<MessageResponse>> CreateSlot(CreateSlotCommand command, CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<MessageResponse>> UpdateSlot(int id, UpdateSlotCommand command, CancellationToken cancellationToken)
    {
        return await sender.Send(command with { Id = id }, cancellationToken);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<MessageResponse>> DeleteSlot(int id, CancellationToken cancellationToken)
    {
        return await sender.Send(new DeleteSlotCommand(id), cancellationToken);
    }
}
