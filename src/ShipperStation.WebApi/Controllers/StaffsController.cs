using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShipperStation.Application.Common.Constants;
using ShipperStation.Application.Features.Stations.Models;
using ShipperStation.Application.Features.Stations.Queries;

namespace ShipperStation.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class StaffsController(ISender sender) : ControllerBase
{
    [HttpGet("stations")]
    [Authorize(Roles = Policies.Staff)]
    public async Task<ActionResult<StationResponse>> GetStationByStaff(CancellationToken cancellationToken)
    {
        return await sender.Send(new GetStationByStaffQuery(), cancellationToken);
    }

}
