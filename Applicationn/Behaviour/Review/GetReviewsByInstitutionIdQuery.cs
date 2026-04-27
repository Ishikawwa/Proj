using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Behaviour.Review
{
    public class GetReviewsByInstitutionIdQuery : IRequest<List<ReviewEntity>>
    {
        public Guid InstitutionId { get; set; }
    }

    public sealed class GetReviewsByInstitutionIdQueryHandler(IReviewRepository repository) : IRequestHandler<GetReviewsByInstitutionIdQuery, List<ReviewEntity>>
    {
        public async Task<List<ReviewEntity>> Handle(GetReviewsByInstitutionIdQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetByInstitutionIdAsync(request.InstitutionId);
        }
    }
}