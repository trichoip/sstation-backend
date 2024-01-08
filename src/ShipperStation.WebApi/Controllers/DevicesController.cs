using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShipperStation.Application.Contracts;
using ShipperStation.Application.Contracts.Devices;
using ShipperStation.Application.Features.Devices.Commands.CreateDevice;
using ShipperStation.Application.Features.Devices.Commands.DeleteDevice;
using ShipperStation.Application.Features.Devices.Commands.UpdateDevice;
using ShipperStation.Application.Features.Devices.Queries.GetDeviceById;
using ShipperStation.Application.Features.Devices.Queries.GetDevices;
using Swashbuckle.AspNetCore.Annotations;

namespace ShipperStation.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
[SwaggerTag("Api for create, read, update, delete user's device FCM token")]
public class DevicesController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Get all device tokens FCM of user
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetDevices(CancellationToken cancellationToken)
    {
        return Ok(await sender.Send(new GetDevicesQuery(), cancellationToken));
    }

    /// <summary>
    /// Get device token FCM by id of user
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<DeviceResponse>> GetDeviceById(int id, CancellationToken cancellationToken)
    {
        return await sender.Send(new GetDeviceByIdQuery(id), cancellationToken);
    }

    /// <summary>
    /// Create new device token FCM for user
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<MessageResponse>> CreateDevice(CreateDeviceCommand command, CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

    /// <summary>
    /// Update device token FCM of user
    /// </summary>
    /// <param name="id"></param>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<MessageResponse>> UpdateDevice(int id, UpdateDeviceCommand command, CancellationToken cancellationToken)
    {
        return await sender.Send(command with { Id = id }, cancellationToken);
    }

    /// <summary>
    /// Delete device token FCM of user
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<MessageResponse>> DeleteDevice(int id, CancellationToken cancellationToken)
    {
        return await sender.Send(new DeleteDeviceCommand(id), cancellationToken);
    }

}
