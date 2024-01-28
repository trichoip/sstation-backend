using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShipperStation.Application.Common.Constants;
using ShipperStation.Application.Features.Pricings.Commands;
using ShipperStation.Application.Features.Pricings.Models;
using ShipperStation.Application.Features.Pricings.Queries;
using ShipperStation.Application.Models;

namespace ShipperStation.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = Policies.Admin)]
public class PricingsController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetPricings(CancellationToken cancellationToken)
    {
        return Ok(await sender.Send(new GetPricingsQuery(), cancellationToken));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PricingResponse>> GetPricingById(int id, CancellationToken cancellationToken)
    {
        return await sender.Send(new GetPricingByIdQuery(id), cancellationToken);
    }

    [HttpPost]
    public async Task<ActionResult<MessageResponse>> CreatePricing(CreatePricingCommand command, CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<MessageResponse>> UpdatePricing(int id, UpdatePricingCommand command, CancellationToken cancellationToken)
    {
        return await sender.Send(command with { Id = id }, cancellationToken);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<MessageResponse>> DeletePricing(int id, CancellationToken cancellationToken)
    {
        return await sender.Send(new DeletePricingCommand(id), cancellationToken);
    }

}
