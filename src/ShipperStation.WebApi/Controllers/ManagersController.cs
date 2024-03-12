using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShipperStation.Application.Common.Constants;
using ShipperStation.Application.Features.Managers.Commands;
using ShipperStation.Application.Features.Stations.Commands;
using ShipperStation.Application.Features.Stations.Models;
using ShipperStation.Application.Features.Stations.Queries;
using ShipperStation.Application.Models;
using ShipperStation.Shared.Pages;

namespace ShipperStation.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ManagersController(ISender sender) : ControllerBase
{
    [Authorize(Roles = Policies.Admin)]
    [HttpPost]
    public async Task<ActionResult<MessageResponse>> CreateStoreManager(
        CreateStoreManagerCommand request,
        CancellationToken cancellationToken)
    {
        return await sender.Send(request, cancellationToken);
    }

    [Authorize(Roles = Policies.StationManager)]
    [HttpGet("stations")]
    public async Task<ActionResult<PaginatedResponse<StationResponse>>> GetStationsByStoreManager(
        [FromQuery] GetStationsByStoreManagerQuery request,
        CancellationToken cancellationToken)
    {
        return await sender.Send(request, cancellationToken);
    }

    [Authorize(Roles = Policies.StationManager)]
    [HttpPut("stations/{id}")]
    public async Task<ActionResult<MessageResponse>> UpdateStationsByStoreManager(
        int id,
        UpdateStationsByStoreManagerCommand request,
        CancellationToken cancellationToken)
    {
        return await sender.Send(request with { Id = id }, cancellationToken);
    }

    [Authorize(Roles = Policies.StationManager)]
    [HttpGet("stations/{id}")]
    public async Task<ActionResult<StationResponse>> GetStationByIdForStoreManager(
        int id,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new GetStationByIdForStoreManagerQuery(id), cancellationToken);
    }

}
