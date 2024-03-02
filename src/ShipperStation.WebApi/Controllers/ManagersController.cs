using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShipperStation.Application.Common.Constants;
using ShipperStation.Application.Features.Managers.Commands;
using ShipperStation.Application.Models;

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

}
