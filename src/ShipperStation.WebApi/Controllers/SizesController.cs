using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShipperStation.Application.Common.Constants;
using ShipperStation.Application.Features.Sizes.Commands;
using ShipperStation.Application.Features.Sizes.Models;
using ShipperStation.Application.Features.Sizes.Queries;
using ShipperStation.Application.Models;

namespace ShipperStation.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
//[SwaggerTag("Api for create, read, update, delete size's")]
[Authorize(Roles = Policies.Admin)]
public class SizesController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetSizes(CancellationToken cancellationToken)
    {
        return Ok(await sender.Send(new GetSizesQuery(), cancellationToken));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SizeResponse>> GetSizeById(int id, CancellationToken cancellationToken)
    {
        return await sender.Send(new GetSizeByIdQuery(id), cancellationToken);
    }

    [HttpPost]
    public async Task<ActionResult<MessageResponse>> CreateSize(CreateSizeCommand command, CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<MessageResponse>> UpdateSize(int id, UpdateSizeCommand command, CancellationToken cancellationToken)
    {
        return await sender.Send(command with { Id = id }, cancellationToken);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<MessageResponse>> DeleteSize(int id, CancellationToken cancellationToken)
    {
        return await sender.Send(new DeleteSizeCommand(id), cancellationToken);
    }
}
