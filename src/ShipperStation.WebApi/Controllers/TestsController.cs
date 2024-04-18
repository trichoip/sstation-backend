using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Contracts.Services.Notifications;
using ShipperStation.Application.Features.PackageFeature.Models;
using ShipperStation.Application.Models.Notifications;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Enums;
using System.Text.Json;

namespace ShipperStation.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TestsController(
    INotifier notifier,
    ICurrentUserService currentUserService,
    ICacheService cacheService) : ControllerBase
{
    [HttpPost("push-notification")]
    public async Task<IActionResult> Post(CancellationToken cancellationToken)
    {
        var notificationMessage = new NotificationRequest
        {
            Type = NotificationType.SystemStaffCreated,
            UserId = await currentUserService.FindCurrentUserIdAsync(),
            Data = JsonSerializer.Serialize(new
            {
                Id = Guid.NewGuid(),
                Entity = nameof(Package)
            })
        };

        BackgroundJob.Enqueue(() => notifier.NotifyAsync(notificationMessage, true, cancellationToken));

        return Ok("push success");
    }

    [HttpPost("cache/{key}")]
    public async Task<IActionResult> PostCahce(string key, string value, CancellationToken cancellationToken)
    {
        await cacheService.SetAsync(key, value, cancellationToken);
        return Ok(key);
    }

    [HttpGet("cache/{key}")]
    public async Task<IActionResult> GetCache(string key, CancellationToken cancellationToken)
    {
        return Ok(await cacheService.GetAsync<string>(key, cancellationToken));
    }

    [HttpGet("cache/package/{key}")]
    public async Task<IActionResult> GetCachePackage(string key, CancellationToken cancellationToken)
    {
        return Ok(await cacheService.GetAsync<InfoStaffGennerateQrPaymentModel>(key, cancellationToken));
    }
}
