using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Constants;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Review
{
    public record ScoreReviewCommand : IRequest<ResponseContract<ReviewScoreEntity>>
    {
        public Guid ReviewId { get; set; }
        public Guid UserId { get; set; }
        public bool IsLiked { get; set; }
    }

    public class ScoreReviewCommandValidator : AbstractValidator<ScoreReviewCommand>
    {
        public ScoreReviewCommandValidator()
        {
            RuleFor(x => x.ReviewId)
                .NotEmpty().WithMessage("ReviewId обязателен");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId обязателен");
        }
    }

    public sealed class ScoreReviewCommandHandler(
        IReviewScoreRepository reviewScoreRepository,
        IReviewRepository reviewRepository) : IRequestHandler<ScoreReviewCommand, ResponseContract<ReviewScoreEntity>>
    {
        public async Task<ResponseContract<ReviewScoreEntity>> Handle(ScoreReviewCommand request, CancellationToken cancellationToken)
        {
            ReviewEntity review = await reviewRepository.GetByIdAsync(request.ReviewId);

            if (review == null)
            {
                return new ResponseContract<ReviewScoreEntity>(ErrorCodes.ReviewNotFound);
            }

            ReviewScoreEntity entity = new()
            {
                Id = Guid.NewGuid(),
                ReviewId = request.ReviewId,
                UserId = request.UserId,
                IsLiked = request.IsLiked
            };

            await reviewScoreRepository.AddAsync(entity);

            return new ResponseContract<ReviewScoreEntity>(entity);
        }
    }
}