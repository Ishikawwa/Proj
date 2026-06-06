using Application.Utils;
using Domain.Constants;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Review
{
    public record GetReviewsByInstitutionIdQuery : IRequest<ResponseContract<List<ReviewEntity>>>
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

    public sealed class GetReviewsByInstitutionIdQueryHandler(
        IReviewRepository reviewRepository,
        IInstitutionRepository institutionRepository) : IRequestHandler<GetReviewsByInstitutionIdQuery, ResponseContract<List<ReviewEntity>>>
    {
        public async Task<ResponseContract<List<ReviewEntity>>> Handle(GetReviewsByInstitutionIdQuery request, CancellationToken cancellationToken)
        {
            InstitutionEntity institution = await institutionRepository.GetByIdAsync(request.InstitutionId);

            if (institution == null)
            {
                return new ResponseContract<List<ReviewEntity>>(ErrorCodes.InstitutionNotFound);
            }

            List<ReviewEntity> reviews = await reviewRepository.GetByInstitutionIdAsync(request.InstitutionId);

            return new ResponseContract<List<ReviewEntity>>(reviews);
        }
    }
}