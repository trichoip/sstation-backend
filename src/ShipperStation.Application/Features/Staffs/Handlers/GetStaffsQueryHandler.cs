using MediatR;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Contracts.Services;
using ShipperStation.Application.Features.Staffs.Queries;
using ShipperStation.Application.Features.Users.Models;
using ShipperStation.Domain.Entities.Identities;
using ShipperStation.Shared.Pages;

namespace ShipperStation.Application.Features.Staffs.Handlers;
internal sealed class GetStaffsQueryHandler(
    ICurrentUserService currentUserService,
    IUnitOfWork unitOfWork) : IRequestHandler<GetStaffsQuery, PaginatedResponse<UserResponse>>
{
    private readonly IGenericRepository<User> _userRepository = unitOfWork.Repository<User>();

    public async Task<PaginatedResponse<UserResponse>> Handle(GetStaffsQuery request, CancellationToken cancellationToken)
    {
        var userId = await currentUserService.FindCurrentUserIdAsync();

        request = request with { UserId = userId };

        var users = await _userRepository
            .FindAsync<UserResponse>(
                request.PageIndex,
                request.PageSize,
                request.GetExpressions(),
                request.GetOrder(),
                cancellationToken);

        return await users.ToPaginatedResponseAsync();
    }
}
