using MediatR;
using ShipperStation.Application.Common.Exceptions;
using ShipperStation.Application.Common.Resources;
using ShipperStation.Application.Contracts.Repositories;
using ShipperStation.Application.Features.Sizes.Commands;
using ShipperStation.Application.Models;
using ShipperStation.Domain.Entities;

namespace ShipperStation.Application.Features.Sizes.Handlers;
internal sealed class DeleteSizeCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteSizeCommand, MessageResponse>
{
    private readonly IGenericRepository<Size> _sizeRepository = unitOfWork.Repository<Size>();
    public async Task<MessageResponse> Handle(DeleteSizeCommand request, CancellationToken cancellationToken)
    {
        var size = await _sizeRepository
            .FindByAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (size == null)
        {
            throw new NotFoundException(nameof(Size), request.Id);
        }

        await _sizeRepository.DeleteAsync(size);

        await unitOfWork.CommitAsync(cancellationToken);

        return new MessageResponse(Resource.SizeDeletedSuccess);
    }
}
