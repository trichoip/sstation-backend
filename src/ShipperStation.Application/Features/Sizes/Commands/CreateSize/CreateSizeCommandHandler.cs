using Mapster;
using MediatR;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts;
using ShipperStation.Application.Interfaces.Repositories;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Sizes.Commands.CreateSize;
internal sealed class CreateSizeCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateSizeCommand, MessageResponse>
{
    private readonly IGenericRepository<Size> _sizeRepository = unitOfWork.Repository<Size>();
    public async Task<MessageResponse> Handle(CreateSizeCommand request, CancellationToken cancellationToken)
    {
        var size = request.Adapt<Size>();
        await _sizeRepository.CreateAsync(size);
        await unitOfWork.CommitAsync();

        return new MessageResponse(Resource.SizeCreatedSuccess);
    }
}
