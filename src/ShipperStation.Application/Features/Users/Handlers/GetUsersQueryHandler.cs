using MediatR;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Users.Models;
using ShipperStation.Application.Features.Users.Queries;
using ShipperStation.Domain.Entities.Identities;
using ShipperStation.Shared.Pages;

namespace ShipperStation.Application.Features.Users.Handlers;
internal sealed class GetUsersQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetUsersQuery, PaginatedResponse<UserResponse>>
{
    private readonly IGenericRepository<User> _userRepository = unitOfWork.Repository<User>();
    public async Task<PaginatedResponse<UserResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
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
