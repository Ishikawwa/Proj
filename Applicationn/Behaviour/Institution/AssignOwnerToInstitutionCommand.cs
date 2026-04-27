using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Behaviour.Institution
{
    public class AssignOwnerToInstitutionCommand : IRequest
    {
        public Guid InstitutionId { get; set; }
        public Guid OwnerId { get; set; }
    }

    public sealed class AssignOwnerToInstitutionCommandHandler(IInstitutionRepository repository) : IRequestHandler<AssignOwnerToInstitutionCommand>
    {
        public async Task Handle(AssignOwnerToInstitutionCommand request, CancellationToken cancellationToken)
        {
            await repository.AssignOwnerAsync(request.InstitutionId, request.OwnerId);
        }
    }
}