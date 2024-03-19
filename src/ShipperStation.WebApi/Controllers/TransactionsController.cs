using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShipperStation.Application.Features.Transactions.Models;
using ShipperStation.Application.Features.Transactions.Queries;

namespace ShipperStation.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TransactionsController(ISender sender) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetTransactions(
        [FromQuery] GetTransactionsQuery query,
        CancellationToken cancellationToken)
    {
        return Ok(await sender.Send(query, cancellationToken));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TransactionResponse>> GetTransactionById(Guid id, CancellationToken cancellationToken)
    {
        return await sender.Send(new GetTransactionByIdQuery(id), cancellationToken);
    }
}
