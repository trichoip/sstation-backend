using MediatR;
using Microsoft.EntityFrameworkCore;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Dashboards.Models;
using ShipperStation.Application.Features.Dashboards.Queries;
using ShipperStation.Domain.Entities.Identities;

namespace ShipperStation.Application.Features.Dashboards.Handlers;
internal sealed class GetUserCountByMonthQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetUserCountByMonthQuery, IList<UserCountByMonthResponse>>
{

    private readonly IGenericRepository<User> _userRepository = unitOfWork.Repository<User>();

    public async Task<IList<UserCountByMonthResponse>> Handle(GetUserCountByMonthQuery request, CancellationToken cancellationToken)
    {
        var monthsInYear = Enumerable.Range(1, 12);

        var userCounts = await _userRepository.Entities
             .Where(_ => _.CreatedAt.Value.Year == request.Year)
             .GroupBy(u => new { Year = u.CreatedAt.Value.Year, Month = u.CreatedAt.Value.Month })
             .Select(g => new UserCountByMonthResponse
             {
                 Month = g.Key.Month,
                 UserCount = g.Count(),
                 Year = g.Key.Year
             })
             .OrderBy(x => x.Month)
             .ToListAsync();

        var result = monthsInYear
            .Select(month => new UserCountByMonthResponse
            {
                Month = month,
                Year = request.Year,
                UserCount = userCounts.FirstOrDefault(x => x.Month == month)?.UserCount ?? 0
            })
            .ToList();

        return result;
    }
}
