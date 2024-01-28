using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShipperStation.Application.Features.Devices.Commands;
using ShipperStation.Application.Features.Devices.Models;
using ShipperStation.Application.Features.Devices.Queries;
using ShipperStation.Application.Models;

namespace ShipperStation.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
// [SwaggerTag("Api for create, read, update, delete user's device FCM token")]
public class DevicesController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetDevices(CancellationToken cancellationToken)
    {
        return Ok(await sender.Send(new GetDevicesQuery(), cancellationToken));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DeviceResponse>> GetDeviceById(int id, CancellationToken cancellationToken)
    {
        return await sender.Send(new GetDeviceByIdQuery(id), cancellationToken);
    }

    [HttpPost]
    public async Task<ActionResult<MessageResponse>> CreateDevice(CreateDeviceCommand command, CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<MessageResponse>> UpdateDevice(int id, UpdateDeviceCommand command, CancellationToken cancellationToken)
    {
        return await sender.Send(command with { Id = id }, cancellationToken);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<MessageResponse>> DeleteDevice(int id, CancellationToken cancellationToken)
    {
        return await sender.Send(new DeleteDeviceCommand(id), cancellationToken);
    }

    [HttpDelete]
    public async Task<ActionResult<MessageResponse>> DeleteDeviceByToken(DeleteDeviceByTokenCommand command, CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

}
