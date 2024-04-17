using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShipperStation.Application.Common.Constants;
using ShipperStation.Application.Features.PackageStatusHistories.Models;
using ShipperStation.Application.Features.PackageStatusHistories.Queries;
using ShipperStation.Shared.Pages;

namespace ShipperStation.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = Policies.StationManager_Or_Staff_Or_Admin)]
public class PackageStatusHistoriesController(ISender sender) : ControllerBase
{

    [HttpGet]
    public async Task<PaginatedResponse<PackageStatusHistoryResponse>> GetPackageStatusHistories(
        [FromQuery] GetPackageStatusHistoriesQuery query,
        CancellationToken cancellationToken)
    {
        return await sender.Send(query, cancellationToken);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PackageStatusHistoryResponse>> GetPackageStatusHistoryById(int id, CancellationToken cancellationToken)
    {
        return await sender.Send(new GetPackageStatusHistoryByIdQuery(id), cancellationToken);
    }
}
