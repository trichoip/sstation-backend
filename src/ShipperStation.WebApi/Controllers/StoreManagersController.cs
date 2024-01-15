using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShipperStation.Application.Common.Constants;
using ShipperStation.Application.Contracts;
using ShipperStation.Application.Contracts.Stations;
using ShipperStation.Application.Contracts.Users;
using ShipperStation.Application.Features.StoreManagers.Commands.CreateStaff;
using ShipperStation.Application.Features.StoreManagers.Commands.CreateStation;
using ShipperStation.Application.Features.StoreManagers.Commands.CreateStoreManager;
using ShipperStation.Application.Features.StoreManagers.Queries.GetStaffs;
using ShipperStation.Application.Features.StoreManagers.Queries.GetStationsByStoreManager;
using ShipperStation.Shared.Pages;
using Swashbuckle.AspNetCore.Annotations;

namespace ShipperStation.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
[SwaggerTag("Api for store manage")]
public class StoreManagersController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Create a store manager account by admin
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Policy = Policies.Admin)]
    [HttpPost]
    public async Task<ActionResult<MessageResponse>> CreateStoreManager(
        CreateStoreManagerCommand request,
        CancellationToken cancellationToken)
    {
        return await sender.Send(request, cancellationToken);
    }

    /// <summary>
    /// Get the store manager's station list
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Policy = Policies.StoreManager)]
    [HttpGet("stations")]
    public async Task<ActionResult<PaginatedResponse<StationResponse>>> GetStationsByStoreManager(
        [FromQuery] GetStationsByStoreManagerQuery request,
        CancellationToken cancellationToken)
    {
        return await sender.Send(request, cancellationToken);
    }

    /// <summary>
    /// Create a staff account and add it to the station by the manager
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Policy = Policies.StoreManager)]
    [HttpPost("stations/{id}/staff")]
    public async Task<ActionResult<MessageResponse>> CreateStaff(
        int id,
        CreateStaffCommand request,
        CancellationToken cancellationToken)
    {
        request = request with
        {
            StationId = id
        };

        return await sender.Send(request, cancellationToken);
    }

    /// <summary>
    /// Get the staff list of the station by the manager
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Policy = Policies.StoreManager)]
    [HttpGet("stations/{id}/staff")]
    public async Task<ActionResult<PaginatedResponse<UserResponse>>> GetStaffs(
        int id,
        [FromQuery] GetStaffsQuery request,
        CancellationToken cancellationToken)
    {
        request = request with
        {
            StationId = id
        };

        return await sender.Send(request, cancellationToken);
    }

    [Authorize(Policy = Policies.StoreManager)]
    [HttpPost("stations")]
    public async Task<ActionResult<MessageResponse>> CreateStation(CreateStationCommand command, CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }
}
