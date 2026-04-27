using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Behaviour.Institution
{
    public class GetInstitutionByIdCommand : IRequest<InstitutionEntity>
    {
        public Guid Id { get; set; }
    }

    public sealed class GetInstitutionByIdQueryHandler(IInstitutionRepository repository) : IRequestHandler<GetInstitutionByIdCommand, InstitutionEntity>
    {
        public async Task<InstitutionEntity> Handle(GetInstitutionByIdCommand request, CancellationToken cancellationToken)
        {
            return await repository.GetByIdAsync(request.Id);
        }
    }
}