using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Behaviour.Institution
{
    public class GetInstitutionQuery : IRequest<List<InstitutionEntity>>
    {
    }

    public sealed class GetInstitutionsQueryHandler(IInstitutionRepository repository) : IRequestHandler<GetInstitutionQuery, List<InstitutionEntity>>
    {
        public async Task<List<InstitutionEntity>> Handle(GetInstitutionQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetAllAsync();
        }
    }
}