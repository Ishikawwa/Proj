using Application.Utils;
using Domain.Entities;
using MediatR;

namespace Application.Behaviour.Institution
{
    public record GetInstitutionQuery : IRequest<ResponseContract<List<InstitutionEntity>>>
    {
    }

    public sealed class GetInstitutionsQueryHandler(IInstitutionRepository institutionRepository) : IRequestHandler<GetInstitutionQuery, ResponseContract<List<InstitutionEntity>>>
    {
        public async Task<ResponseContract<List<InstitutionEntity>>> Handle(GetInstitutionQuery request, CancellationToken cancellationToken)
        {
            await institutionRepository.GetAllAsync();

            return new ResponseContract<List<InstitutionEntity>>((List<InstitutionEntity>)institutionRepository);
        }
    }
}