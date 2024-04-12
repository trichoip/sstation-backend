using MediatR;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Dashboards.Models;
using ShipperStation.Application.Features.Dashboards.Queries;
using ShipperStation.Domain.Entities;
using ShipperStation.Domain.Entities.Identities;
using ShipperStation.Domain.Enums;

namespace ShipperStation.Application.Features.Dashboards.Handlers;
internal sealed class GetInfomationDashBoardQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetInfomationDashBoardQuery, IList<GetInfomationDashBoardModel>>
{
    private readonly IGenericRepository<Payment> _paymentRepository = unitOfWork.Repository<Payment>();
    private readonly IGenericRepository<User> _userRepository = unitOfWork.Repository<User>();

    public async Task<IList<GetInfomationDashBoardModel>> Handle(GetInfomationDashBoardQuery request, CancellationToken cancellationToken)
    {
        var result = new List<GetInfomationDashBoardModel>();

        var paymentToday = await _paymentRepository
            .FindAsync(_ => _.CreatedAt.Value.Day == DateTimeOffset.UtcNow.Day && _.Status == PaymentStatus.Success);

        var totalSalesToday = paymentToday.Sum(x => x.ServiceFee);

        var paymentYesterday = await _paymentRepository.
            FindAsync(_ => _.CreatedAt.Value.Day == DateTimeOffset.UtcNow.Day - 1 && _.Status == PaymentStatus.Success);

        var totalSalesYesterday = paymentYesterday.Sum(x => x.ServiceFee);

        var totalSales = 0.0;

        if (totalSalesYesterday != 0)
        {
            totalSales = Math.Round((double)(totalSalesToday - totalSalesYesterday) / totalSalesYesterday * 100);
        }
        else
        {
            if (totalSalesToday == 0)
            {
                totalSales = 0;
            }
            else
            {
                totalSales = 100;
            }

        }

        result.Add(new GetInfomationDashBoardModel
        {
            DashBoardType = DashBoardType.TodaySales,
            Percent = totalSales,
            Value = totalSalesToday
        });

        var userToday = await _userRepository
            .FindAsync(_ => _.CreatedAt.Value.Day == DateTimeOffset.UtcNow.Day);

        var totalUserToday = userToday.Count();

        var userYesterday = await _userRepository
            .FindAsync(_ => _.CreatedAt.Value.Day == DateTimeOffset.UtcNow.Day - 1);

        var totalUserYesterday = userYesterday.Count();

        var totalUser = 0.0;

        if (totalUserYesterday != 0)
        {
            totalUser = Math.Round((double)(totalUserToday - totalUserYesterday) / totalUserYesterday * 100);
        }
        else
        {

            if (totalUserToday == 0)
            {
                totalUser = 0;
            }
            else
            {
                totalUser = 100;
            }
        }

        result.Add(new GetInfomationDashBoardModel
        {
            DashBoardType = DashBoardType.TodayUsers,
            Percent = totalUser,
            Value = totalUserToday
        });

        var newClientToday = await _userRepository
            .FindAsync(_ => _.CreatedAt.Value.Month == DateTimeOffset.UtcNow.Month);

        var totalNewClientToday = newClientToday.Count();

        var newClientYesterday = await _userRepository
            .FindAsync(_ => _.CreatedAt.Value.Month == DateTimeOffset.UtcNow.Month - 1);

        var totalNewClientYesterday = newClientYesterday.Count();

        double totalNewClient = 0.0;
        if (totalNewClientYesterday != 0)
        {
            totalNewClient = Math.Round((double)(totalNewClientToday - totalNewClientYesterday) / totalNewClientYesterday * 100);
        }
        else
        {

            if (totalNewClientToday == 0)
            {
                totalNewClient = 0;
            }
            else
            {
                totalNewClient = 100;
            }
        }

        result.Add(new GetInfomationDashBoardModel
        {
            DashBoardType = DashBoardType.NewClient,
            Percent = totalNewClient,
            Value = totalNewClientToday
        });

        var newOrdersToday = await _paymentRepository
            .FindAsync(_ => _.CreatedAt.Value.Month == DateTimeOffset.UtcNow.Month);

        var totalNewOrdersToday = newOrdersToday.Sum(x => x.ServiceFee);

        var newOrdersYesterday = await _paymentRepository
            .FindAsync(_ => _.CreatedAt.Value.Month == DateTimeOffset.UtcNow.Month - 1);

        var totalNewOrdersYesterday = newOrdersYesterday.Sum(x => x.ServiceFee);

        var totalNewOrders = 0.0;

        if (totalNewOrdersYesterday != 0)
        {
            totalNewOrders = Math.Round((double)(totalNewOrdersToday - totalNewOrdersYesterday) / totalNewOrdersYesterday * 100);
        }
        else
        {

            if (totalNewOrdersToday == 0)
            {
                totalNewOrders = 0;
            }
            else
            {
                totalNewOrders = 100;
            }
        }

        result.Add(new GetInfomationDashBoardModel
        {
            DashBoardType = DashBoardType.NewOrders,
            Percent = totalNewOrders,
            Value = totalNewOrdersToday
        });

        return result;

    }
}
