using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShipperStation.Application.Features.DefaultPricings.Commands;
using ShipperStation.Application.Features.DefaultPricings.Models;
using ShipperStation.Application.Features.DefaultPricings.Queries;
using ShipperStation.Application.Models;
using ShipperStation.Shared.Pages;

namespace ShipperStation.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DefaultPricingsController(ISender sender) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<PaginatedResponse<DefaultPricingResponse>>> GetDefaultPricings(
         [FromQuery] GetDefaultPricingsQuery query,
         CancellationToken cancellationToken)
    {
        return await sender.Send(query, cancellationToken);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DefaultPricingResponse>> GetDefaultPricingById(int id, CancellationToken cancellationToken)
    {
        return await sender.Send(new GetDefaultPricingByIdQuery(id), cancellationToken);
    }

    [HttpPost]
    public async Task<ActionResult<MessageResponse>> CreateDefaultPricing(CreateDefaultPricingCommand command, CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<MessageResponse>> UpdateDefaultPricing(int id, UpdateDefaultPricingCommand command, CancellationToken cancellationToken)
    {
        return await sender.Send(command with { Id = id }, cancellationToken);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<MessageResponse>> DeleteDefaultPricing(int id, CancellationToken cancellationToken)
    {
        return await sender.Send(new DeleteDefaultPricingCommand(id), cancellationToken);
    }
}
