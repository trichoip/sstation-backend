using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShipperStation.Application.Common.Constants;
using ShipperStation.Application.Contracts;
using ShipperStation.Application.Contracts.Sizes;
using ShipperStation.Application.Features.Sizes.Commands.CreateSize;
using ShipperStation.Application.Features.Sizes.Commands.DeleteSize;
using ShipperStation.Application.Features.Sizes.Commands.UpdateSize;
using ShipperStation.Application.Features.Sizes.Queries.GetSizeById;
using ShipperStation.Application.Features.Sizes.Queries.GetSizes;
using Swashbuckle.AspNetCore.Annotations;

namespace ShipperStation.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
[SwaggerTag("Api for create, read, update, delete size's")]
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
