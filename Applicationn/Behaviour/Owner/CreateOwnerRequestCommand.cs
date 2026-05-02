using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Behaviour.OwnerRequest
{
    public record CreateOwnerRequestCommand : IRequest<OwnerRequestEntity>
    {
        public Guid UserId { get; set; }
        public Guid InstitutionId { get; set; }
        public string Comment { get; set; }
    }

    public sealed class CreateOwnerRequestCommandHandler(IOwnerRequestRepository repository) : IRequestHandler<CreateOwnerRequestCommand, OwnerRequestEntity>
    {
        public async Task<OwnerRequestEntity> Handle(CreateOwnerRequestCommand request, CancellationToken cancellationToken)
        {
            OwnerRequestEntity entity = new()
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                InstitutionId = request.InstitutionId,
                Comment = request.Comment,
            };

            await repository.AddAsync(entity);

            return entity;
        }
    }
}