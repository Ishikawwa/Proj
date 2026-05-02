using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Behaviour.Institution
{
    public record GetInstitutionByOwnerIdQuery : IRequest<List<InstitutionEntity>>
    {
        public Guid OwnerId { get; set; }
    }

    public sealed class GetInstitutionByOwnerIdQueryHandler(IInstitutionRepository repository) : IRequestHandler<GetInstitutionByOwnerIdQuery, List<InstitutionEntity>>
    {
        public async Task<List<InstitutionEntity>> Handle(GetInstitutionByOwnerIdQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetByOwnerIdAsync(request.OwnerId);
        }
    }
}