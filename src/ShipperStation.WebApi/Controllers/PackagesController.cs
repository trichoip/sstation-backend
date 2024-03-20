using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShipperStation.Application.Common.Constants;
using ShipperStation.Application.Features.PackageFeature.Commands;
using ShipperStation.Application.Features.PackageFeature.Models;

namespace ShipperStation.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PackagesController(ISender sender) : ControllerBase
{

    [HttpPost]
    [Authorize(Roles = Policies.StationManager_Or_Staff)]
    public async Task<ActionResult<PackageResponse>> CreatePackage(
        CreatePackageCommand command,
        CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

    //[HttpPost("{id}/return")]
    //[Authorize]
    //public async Task<ActionResult<MessageResponse>> ReturnPackage(
    //    Guid id,
    //    ReturnPackageCommand command,
    //    CancellationToken cancellationToken)
    //{
    //    return await sender.Send(command with { Id = id }, cancellationToken);
    //}

    //[HttpPost("{id}/confirm")]
    //[Authorize]
    //public async Task<ActionResult<MessageResponse>> ConfirmPackage(
    //    Guid id,
    //    ConfirmPackageCommand command,
    //    CancellationToken cancellationToken)
    //{
    //    return await sender.Send(command with { Id = id }, cancellationToken);
    //}

    //[HttpPut("{id}")]
    //public async Task<ActionResult<MessageResponse>> UpdatePackage(int id, UpdatePackageCommand command, CancellationToken cancellationToken)
    //{
    //    return await sender.Send(command with { Id = id }, cancellationToken);
    //}

    //[HttpDelete("{id}")]
    //public async Task<ActionResult<MessageResponse>> DeletePackage(int id, CancellationToken cancellationToken)
    //{
    //    return await sender.Send(new DeletePackageCommand(id), cancellationToken);
    //}
}
