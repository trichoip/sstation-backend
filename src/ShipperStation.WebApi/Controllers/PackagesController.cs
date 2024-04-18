using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShipperStation.Application.Common.Constants;
using ShipperStation.Application.Features.PackageFeature.Commands;
using ShipperStation.Application.Features.PackageFeature.Models;
using ShipperStation.Application.Features.PackageFeature.Queries;
using ShipperStation.Application.Models;
using ShipperStation.Shared.Pages;

namespace ShipperStation.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PackagesController(ISender sender) : ControllerBase
{
    [Authorize(Roles = Policies.StationManager_Or_Staff_Or_Admin)]
    [HttpPost]
    public async Task<ActionResult<PackageResponse>> CreatePackage(
        CreatePackageCommand command,
        CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

    [Authorize(Roles = Policies.StationManager_Or_Staff_Or_Admin)]
    [HttpGet]
    public async Task<ActionResult<PaginatedResponse<PackageResponse>>> GetPackages(
       [FromQuery] GetPackagesQuery query,
       CancellationToken cancellationToken)
    {
        return await sender.Send(query, cancellationToken);
    }

    [Authorize(Roles = Policies.StationManager_Or_Staff_Or_Admin)]
    [HttpGet("{id}")]
    public async Task<ActionResult<PackageResponse>> GetPackageById(Guid id, CancellationToken cancellationToken)
    {
        return await sender.Send(new GetPackageByIdQuery(id), cancellationToken);
    }

    [Authorize(Roles = Policies.StationManager_Or_Staff_Or_Admin)]
    [HttpPost("{id}/return")]
    public async Task<ActionResult<MessageResponse>> ReturnPackage(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new ReturnPackageCommand() with { Id = id }, cancellationToken);
    }

    [Authorize(Roles = Policies.StationManager_Or_Staff_Or_Admin)]
    [HttpPost("{id}/confirm")]
    public async Task<ActionResult<MessageResponse>> ConfirmPackage(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new ConfirmPackageCommand() with { Id = id }, cancellationToken);
    }

    [Authorize(Roles = Policies.StationManager_Or_Staff_Or_Admin)]
    [HttpPut("{id}")]
    public async Task<ActionResult<MessageResponse>> UpdatePackage(
        Guid id,
        UpdatePackageCommand command,
        CancellationToken cancellationToken)
    {
        return await sender.Send(command with { Id = id }, cancellationToken);
    }

    [Authorize(Roles = Policies.StationManager_Or_Staff_Or_Admin)]
    [HttpPatch("{id}/change-location")]
    public async Task<ActionResult<MessageResponse>> UpdateLocationPackage(
        Guid id,
        UpdateLocationPackageCommand command,
        CancellationToken cancellationToken)
    {
        return await sender.Send(command with { Id = id }, cancellationToken);
    }

    [Authorize(Roles = Policies.StationManager_Or_Staff_Or_Admin)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<MessageResponse>> DeletePackage(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new DeletePackageCommand(id), cancellationToken);
    }

    [Authorize(Roles = Policies.StationManager_Or_Staff_Or_Admin)]
    [HttpPost("force")]
    public async Task<ActionResult<PackageResponse>> ForceCreatePackage(
        ForceCreatePackageCommand command,
        CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

    [Authorize(Roles = Policies.StationManager_Or_Staff_Or_Admin)]
    [HttpGet("{id}/qr-payment")]
    public async Task<ActionResult<QrPaymentPackage>> GetQrPaymentPackagePackage(
       Guid id,
       CancellationToken cancellationToken)
    {
        return await sender.Send(new GetQrPaymentPackageQuery() with { Id = id }, cancellationToken);
    }

    [Authorize(Roles = Policies.StationManager_Or_Staff_Or_Admin)]
    [HttpPost("expire")]
    public async Task<ActionResult<MessageResponse>> ExpirePackage(
        ExpirePackageCommand command,
        CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

    [Authorize(Roles = Policies.StationManager_Or_Staff_Or_Admin)]
    [HttpPost("push-notication/receive")]
    public async Task<ActionResult<MessageResponse>> PushNoticationReceivePackage(
        PushNoticationReceivePackageCommand command,
        CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

    [Authorize]
    [HttpPost("{id}/cancel")]
    public async Task<ActionResult<MessageResponse>> CancelPackage(
        Guid id,
        CancelPackageCommand command,
        CancellationToken cancellationToken)
    {
        return await sender.Send(command with { Id = id }, cancellationToken);
    }

    [Authorize]
    [HttpPost("{id}/payment")]
    public async Task<ActionResult<MessageResponse>> PaymentPackage(
       Guid id,
       PaymentPackageCommand command,
       CancellationToken cancellationToken)
    {
        return await sender.Send(command with { Id = id }, cancellationToken);
    }
}
