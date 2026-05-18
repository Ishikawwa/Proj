using Application.Utils;
using Domain.Constants;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Review
{
    public record BanReviewCommand : IRequest<ResponseContract<Unit>>
    {
        public Guid Id { get; set; }
        public bool IsBanned { get; set; }
    }

    public class BanReviewCommandValidator : AbstractValidator<BanReviewCommand>
    {
        public BanReviewCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id отзыва обязателен");
        }
    }

    public sealed class BanReviewCommandHandler(IReviewRepository reviewRepository) : IRequestHandler<BanReviewCommand, ResponseContract<Unit>>
    {
        public async Task<ResponseContract<Unit>> Handle(BanReviewCommand request, CancellationToken cancellationToken)
        {
            ReviewEntity review = await reviewRepository.GetByIdAsync(request.Id);

            if (review == null)
            {
                return new ResponseContract<Unit>(ErrorCodes.ReviewNotFound);
            }

            if (review.IsBanned)
            {
                return new ResponseContract<Unit>(ErrorCodes.ReviewIsBanned);
            }

            await reviewRepository.BanAsync(request.Id);

            return new ResponseContract<Unit>(Unit.Value);
        }
    }
}