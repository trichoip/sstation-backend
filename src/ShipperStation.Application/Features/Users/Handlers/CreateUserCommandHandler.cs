using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Users.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Constants;
using ShipperStation.Domain.Entities.Identities;

namespace ShipperStation.Application.Features.Users.Handlers;
internal sealed class CreateUserCommandHandler(
    IUnitOfWork unitOfWork,
    UserManager<User> userManager) : IRequestHandler<CreateUserCommand, MessageResponse>
{
    private readonly IGenericRepository<User> _userRepository = unitOfWork.Repository<User>();
    public async Task<MessageResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.ExistsByAsync(_ => _.PhoneNumber == request.PhoneNumber))
        {
            throw new ConflictException(nameof(User), request.PhoneNumber);
        }

        var user = request.Adapt<User>();
        user.UserName = request.PhoneNumber;
        user.IsActive = true;
        user.PhoneNumberConfirmed = true;

        //TODO:  userManager.CreateAsync có check duplicate username
        var result = await userManager.CreateAsync(user);

        if (!result.Succeeded)
        {
            throw new ValidationBadRequestException(result.Errors);
        }

        result = await userManager.AddToRoleAsync(user, RoleName.User);

        if (!result.Succeeded)
        {
            throw new ValidationBadRequestException(result.Errors);
        }

        return new MessageResponse(Resource.CreatedSuccess);
    }
}
