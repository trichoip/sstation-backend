using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShipperStation.Application.Common.Constants;
using ShipperStation.Application.Features.Dashboards.Queries;
using ShipperStation.Application.Features.Stations.Commands;
using ShipperStation.Application.Features.Stations.Models;
using ShipperStation.Application.Features.Stations.Queries;
using ShipperStation.Application.Features.Transactions.Models;
using ShipperStation.Application.Features.Transactions.Queries;
using ShipperStation.Application.Features.Users.Commands;
using ShipperStation.Application.Features.Users.Models;
using ShipperStation.Application.Features.Users.Queries;
using ShipperStation.Application.Features.UserStations.Commands;
using ShipperStation.Application.Features.UserStations.Queries;
using ShipperStation.Application.Models;
using ShipperStation.Shared.Pages;

namespace ShipperStation.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = Policies.Admin)]
public class AdminController(ISender sender) : ControllerBase
{
    #region Stations
    [HttpPost("stations")]
    public async Task<ActionResult<StationResponse>> CreateStation(
       CreateStationCommand command,
       CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

    [HttpGet("stations")]
    public async Task<ActionResult<PaginatedResponse<StationResponse>>> GetAllStations(
        [FromQuery] GetAllStationsQuery request,
        CancellationToken cancellationToken)
    {
        return await sender.Send(request, cancellationToken);
    }

    [HttpGet("stations/{id}")]
    public async Task<ActionResult<StationResponse>> GetStationByIdForAdmin(
        int id,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new GetStationByIdForAdminQuery(id), cancellationToken);
    }

    [HttpDelete("stations/{id}")]
    public async Task<ActionResult<MessageResponse>> DeleteStationByAdmin(
        int id,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new DeleteStationByAdminCommand(id), cancellationToken);
    }

    [HttpPut("stations/{id}")]
    public async Task<ActionResult<MessageResponse>> UpdateStationByAdmin(
        int id,
        UpdateStationByAdminCommand request,
        CancellationToken cancellationToken)
    {
        return await sender.Send(request with { Id = id }, cancellationToken);
    }

    [HttpPatch("stations/{id}/browse")]
    public async Task<ActionResult<MessageResponse>> BrowseStation(
       int id,
       BrowseStationCommand command,
       CancellationToken cancellationToken)
    {
        return await sender.Send(command with { Id = id }, cancellationToken);
    }

    #endregion

    #region Manager in stations

    [HttpGet("stations/{stationId}/managers")]
    public async Task<ActionResult<PaginatedResponse<UserResponse>>> GetManagersInStation(
        int stationId,
        [FromQuery] GetManagersInStationQuery request,
        CancellationToken cancellationToken)
    {
        return await sender.Send(request with { StationId = stationId }, cancellationToken);
    }

    [HttpGet("stations/{stationId}/managers/{managerId}")]
    public async Task<ActionResult<UserResponse>> GetManagerInStation(
        int stationId,
        Guid managerId,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new GetManagerInStationQuery(managerId) with { StationId = stationId }, cancellationToken);
    }

    [HttpDelete("stations/{stationId}/managers/{managerId}")]
    public async Task<ActionResult<MessageResponse>> DeleteManagerInStation(
        int stationId,
        Guid managerId,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new DeleteManagerInStationCommand(managerId) with { StationId = stationId }, cancellationToken);
    }

    [HttpPost("stations/{stationId}/managers")]
    public async Task<ActionResult<MessageResponse>> CreateManagerIntoStation(
       int stationId,
       CreateManagerIntoStationCommand command,
       CancellationToken cancellationToken)
    {
        return await sender.Send(command with { StationId = stationId }, cancellationToken);
    }

    #endregion

    [HttpGet("user-count-by-month")]
    public async Task<IActionResult> GetUserCountByMonth(
        [FromQuery] GetUserCountByMonthQuery request,
        CancellationToken cancellationToken)
    {
        return Ok(await sender.Send(request, cancellationToken));
    }
    #region User

    [HttpGet("users")]
    public async Task<ActionResult<PaginatedResponse<UserResponse>>> GetUsers(
         [FromQuery] GetUsersForAdminQuery query,
         CancellationToken cancellationToken)
    {
        return await sender.Send(query, cancellationToken);
    }

    [HttpGet("users/{id}")]
    public async Task<ActionResult<UserResponse>> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        return await sender.Send(new GetUserByIdForAdminQuery(id), cancellationToken);
    }

    [HttpPut("users/{id}")]
    public async Task<ActionResult<MessageResponse>> UpdateUser(Guid id, UpdateUserForAdminCommand command, CancellationToken cancellationToken)
    {
        return await sender.Send(command with { Id = id }, cancellationToken);
    }

    [HttpDelete("users/{id}")]
    public async Task<ActionResult<MessageResponse>> DeleteUser(Guid id, CancellationToken cancellationToken)
    {
        return await sender.Send(new DeleteUserForAdminCommand(id), cancellationToken);
    }

    #endregion

    #region Transactions
    [HttpGet("transactions")]
    public async Task<ActionResult<PaginatedResponse<TransactionResponse>>> GetTransactions(
        [FromQuery] GetTransactionsForAdminQuery query,
        CancellationToken cancellationToken)
    {
        return await sender.Send(query, cancellationToken);
    }

    [HttpGet("transactions/{id}")]
    public async Task<ActionResult<TransactionResponse>> GetTransactionById(Guid id, CancellationToken cancellationToken)
    {
        return await sender.Send(new GetTransactionByIdForAdminQuery(id), cancellationToken);
    }
    #endregion

    #region Get station of manager
    [HttpGet("managers/{managerId}/stations")]
    public async Task<ActionResult<PaginatedResponse<StationResponse>>> GetStationsOfManager(
       Guid managerId,
       [FromQuery] GetStationsOfManagerQuery request,
       CancellationToken cancellationToken)
    {
        return await sender.Send(request with { ManagerId = managerId }, cancellationToken);
    }

    [HttpGet("managers/{managerId}/stations/{stationId}")]
    public async Task<ActionResult<StationResponse>> GetStationOfManager(
        int stationId,
        Guid managerId,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new GetStationOfManagerQuery(stationId) with { ManagerId = managerId }, cancellationToken);
    }

    #endregion
}
