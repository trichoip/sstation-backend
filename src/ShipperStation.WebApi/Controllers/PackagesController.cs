using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShipperStation.Application.Common.Constants;
using ShipperStation.Application.Features.PackageFeature.Commands;
using ShipperStation.Application.Features.PackageFeature.Models;
using ShipperStation.Application.Features.PackageFeature.Queries;
using ShipperStation.Shared.Pages;

namespace ShipperStation.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = Policies.StationManager_Or_Staff)]
public class PackagesController(ISender sender) : ControllerBase
{

    [HttpPost]
    public async Task<ActionResult<PackageResponse>> CreatePackage(
        CreatePackageCommand command,
        CancellationToken cancellationToken)
    {
        return await sender.Send(command, cancellationToken);
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResponse<PackageResponse>>> GetPackages(
       [FromQuery] GetPackagesQuery query,
       CancellationToken cancellationToken)
    {
        return await sender.Send(query, cancellationToken);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PackageResponse>> GetPackageById(Guid id, CancellationToken cancellationToken)
    {
        return await sender.Send(new GetPackageByIdQuery(id), cancellationToken);
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
