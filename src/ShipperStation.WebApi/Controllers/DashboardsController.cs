using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Dashboards.Queries;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Entities.Identities;

namespace ShipperStation.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DashboardsController(ISender sender, IUnitOfWork unitOfWork) : ControllerBase
{
    private readonly IGenericRepository<Station> _stationRepository = unitOfWork.Repository<Station>();
    private readonly IGenericRepository<Package> _packageRepository = unitOfWork.Repository<Package>();
    private readonly IGenericRepository<Payment> _paymentRepository = unitOfWork.Repository<Payment>();
    private readonly IGenericRepository<User> _userRepository = unitOfWork.Repository<User>();
    private readonly IGenericRepository<UserStation> _userStationRepository = unitOfWork.Repository<UserStation>();

    [HttpGet]
    public async Task<IActionResult> GetInfomationDashBoard(
        [FromQuery] GetInfomationDashBoardQuery request,
        CancellationToken cancellationToken)
    {
        return Ok(await sender.Send(request, cancellationToken));
    }

    [HttpGet("total-package")]
    public async Task<IActionResult> TotalNumberPackageIntoStation(int? stationId, CancellationToken cancellationToken)
    {
        var number = await _packageRepository.CountAsync(x => !stationId.HasValue || x.Station.Id == stationId, cancellationToken);

        return Ok(number);
    }

    [HttpGet("total-revenue")]
    public async Task<IActionResult> TotalRevenueStation(int? stationId, CancellationToken cancellationToken)
    {
        var number = await _paymentRepository.FindAsync(x => !stationId.HasValue || x.StationId == stationId, cancellationToken: cancellationToken);

        return Ok(number.Sum(_ => _.ServiceFee));
    }

    [HttpGet("number-staff")]
    public async Task<IActionResult> TotalStaff(int? stationId, CancellationToken cancellationToken)
    {
        var number = await _userStationRepository.CountAsync(x => !stationId.HasValue || x.StationId == stationId, cancellationToken: cancellationToken);

        return Ok(number);
    }

    [HttpGet("statistical-revenue")]
    public async Task<IActionResult> StatisticalRevenue(int? stationId, int year, CancellationToken cancellationToken)
    {
        var monthsInYear = Enumerable.Range(1, 12);

        var number = await _paymentRepository.Entities
            .Where(x => (!stationId.HasValue || x.Station.Id == stationId) && x.CreatedAt.Value.Year == year)
            .GroupBy(u => new { Year = u.CreatedAt.Value.Year, Month = u.CreatedAt.Value.Month })
            .Select(g => new
            {
                Revenue = g.Sum(_ => _.ServiceFee),
                Year = g.Key.Year,
                Month = g.Key.Month
            })
            .OrderBy(x => x.Month)
            .ToListAsync(cancellationToken);

        var result = monthsInYear.Select(month => new
        {
            Revenue = number.FirstOrDefault(x => x.Month == month)?.Revenue ?? 0,
            Year = year,
            Month = month
        }).ToList();

        return Ok(result);
    }

    [HttpGet("statistical-package")]
    public async Task<IActionResult> StatisticalPackage(int? stationId, int year, CancellationToken cancellationToken)
    {
        var monthsInYear = Enumerable.Range(1, 12);

        var number = await _packageRepository.Entities
            .Where(x => (!stationId.HasValue || x.Station.Id == stationId) && x.CreatedAt.Value.Year == year)
            .GroupBy(u => new { Year = u.CreatedAt.Value.Year, Month = u.CreatedAt.Value.Month })
            .Select(g => new
            {
                PackageCount = g.Count(),
                Year = g.Key.Year,
                Month = g.Key.Month
            })
            .OrderBy(x => x.Month)
            .ToListAsync(cancellationToken);

        var result = monthsInYear.Select(month => new
        {
            PackageCount = number.FirstOrDefault(x => x.Month == month)?.PackageCount ?? 0,
            Year = year,
            Month = month
        }).ToList();

        return Ok(result);
    }

}
