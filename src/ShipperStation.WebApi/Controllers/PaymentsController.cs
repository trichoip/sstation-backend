using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShipperStation.Application.Features.Payments.Commands;
using ShipperStation.Application.Features.Transactions.Models;

namespace ShipperStation.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PaymentsController(ISender sender, IHttpContextAccessor _httpContextAccessor) : ControllerBase
{
    [Authorize]
    [HttpPost("deposit")]
    public async Task<IActionResult> Deposit(DepositCommand command, string returnUrl, CancellationToken cancellationToken)
    {
        return Ok(await sender.Send(command with { returnUrl = returnUrl }, cancellationToken));
    }

    [HttpGet("callback/momo")]
    public async Task<IActionResult> MomoPaymentCallback(
        [FromQuery] MomoPaymentCallbackCommand callback,
        CancellationToken cancellationToken)
    {
        await sender.Send(callback, cancellationToken);
        //return Redirect($"{callback.returnUrl}{HttpUtility.UrlEncode(_httpContextAccessor?.HttpContext?.Request.QueryString.Value)}");
        //return Redirect($"{callback.returnUrl}{HttpUtility.UrlEncode($"?isSuccess={callback.IsSuccess}")}");
        return Redirect($"{callback.returnUrl}?isSuccess={callback.IsSuccess}");
    }

    [HttpGet("callback/vnpay")]
    public async Task<ActionResult<TransactionResponse>> VnPayPaymentCallback(
        [FromQuery] VnPayPaymentCallbackCommand callback,
        CancellationToken cancellationToken)
    {
        await sender.Send(callback, cancellationToken);
        //return Redirect($"{callback.returnUrl}{HttpUtility.UrlEncode(_httpContextAccessor?.HttpContext?.Request.QueryString.Value)}");
        //return Redirect($"{callback.returnUrl}{HttpUtility.UrlEncode($"?isSuccess={callback.IsSuccess}")}");
        return Redirect($"{callback.returnUrl}?isSuccess={callback.IsSuccess}");
    }
}
