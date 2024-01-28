using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Roles.Models;
using ShipperStation.Application.Features.Roles.Queries;
using ShipperStation.Domain.Entities.Identities;

namespace ShipperStation.Application.Features.Roles.Handlers;
internal sealed class GetRoleByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetRoleByIdQuery, RoleResponse>
{
    private readonly IGenericRepository<Role> _roleRepository = unitOfWork.Repository<Role>();
    public async Task<RoleResponse> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.FindByAsync<RoleResponse>(_ => _.Id == request.Id, cancellationToken);

        return role != null ? role : throw new NotFoundException(nameof(Role), request.Id);
    }
}
