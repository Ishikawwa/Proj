using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Review
{
    public record GetReviewsByInstitutionIdQuery : IRequest<List<ReviewEntity>>
    {
        public Guid InstitutionId { get; set; }
    }

    public class GetReviewsByInstitutionIdQueryValidator : AbstractValidator<GetReviewsByInstitutionIdQuery>
    {
        public GetReviewsByInstitutionIdQueryValidator()
        {
            RuleFor(x => x.InstitutionId)
                .NotEmpty().WithMessage("InstitutionId обязателен");
        }
    }

    public sealed class GetReviewsByInstitutionIdQueryHandler(IReviewRepository repository) : IRequestHandler<GetReviewsByInstitutionIdQuery, List<ReviewEntity>>
    {
        public async Task<List<ReviewEntity>> Handle(GetReviewsByInstitutionIdQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetByInstitutionIdAsync(request.InstitutionId);
        }
    }
}