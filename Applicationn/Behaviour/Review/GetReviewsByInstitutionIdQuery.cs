using Application.Utils;
using Domain.Constants;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Review
{
    public record GetReviewsByInstitutionIdQuery : IRequest<ResponseContract<List<Unit>>>
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
        IInstitutionRepository institutionRepository) : IRequestHandler<GetReviewsByInstitutionIdQuery, ResponseContract<List<Unit>>>
    {
        public async Task<ResponseContract<List<Unit>>> Handle(GetReviewsByInstitutionIdQuery request, CancellationToken cancellationToken)
        {
            InstitutionEntity institution = await institutionRepository.GetByIdAsync(request.InstitutionId);

            if (institution == null)
            {
                return new ResponseContract<List<Unit>>(ErrorCodes.InstitutionNotFound);
            }

            if (await reviewRepository.GetByIdAsync(request.InstitutionId) == null)
            {
                return new ResponseContract<List<Unit>>(ErrorCodes.ReviewNotFound);
            }

            await reviewRepository.GetByInstitutionIdAsync(request.InstitutionId);

            return new ResponseContract<List<Unit>>();
        }
    }
}