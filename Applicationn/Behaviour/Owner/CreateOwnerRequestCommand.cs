using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Behaviour.OwnerRequest
{
    public class CreateOwnerRequestCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public Guid InstitutionId { get; set; }
        public string Comment { get; set; }
    }

    public sealed class CreateOwnerRequestCommandHandler(IOwnerRequestRepository repository) : IRequestHandler<CreateOwnerRequestCommand, Guid>
    {
        public async Task<Guid> Handle(CreateOwnerRequestCommand request, CancellationToken cancellationToken)
        {
            OwnerRequestEntity entity = new()
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                InstitutionId = request.InstitutionId,
                Comment = request.Comment
            };

            await repository.AddAsync(entity);

            return entity.Id;
        }
    }
}