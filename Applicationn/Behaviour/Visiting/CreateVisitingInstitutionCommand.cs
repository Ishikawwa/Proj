using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Behaviour.Visiting
{
    public class CreateVisitingInstitutionCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public Guid InstitutionId { get; set; }
    }

    public sealed class CreateVisitingInstitutionCommandHandler(IVisitingRepository repository) : IRequestHandler<CreateVisitingInstitutionCommand, Guid>
    {
        public async Task<Guid> Handle(CreateVisitingInstitutionCommand request, CancellationToken cancellationToken)
        {
            VisitingEntity entity = new()
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                InstitutionId = request.InstitutionId
            };

            await repository.AddAsync(entity);

            return entity.Id;
        }
    }
}