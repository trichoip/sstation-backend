using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShipperStation.Application.Features.Dashboards.Queries;

namespace ShipperStation.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DashboardsController(ISender sender) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetInfomationDashBoard(
        [FromQuery] GetInfomationDashBoardQuery request,
        CancellationToken cancellationToken)
    {
        return Ok(await sender.Send(request, cancellationToken));
    }
}
