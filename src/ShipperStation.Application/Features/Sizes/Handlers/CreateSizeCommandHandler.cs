using Mapster;
using MediatR;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Sizes.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Sizes.Handlers;
internal sealed class CreateSizeCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateSizeCommand, MessageResponse>
{
    private readonly IGenericRepository<Size> _sizeRepository = unitOfWork.Repository<Size>();
    public async Task<MessageResponse> Handle(CreateSizeCommand request, CancellationToken cancellationToken)
    {
        var size = request.Adapt<Size>();
        await _sizeRepository.CreateAsync(size, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return new MessageResponse(Resource.SizeCreatedSuccess);
    }
}
