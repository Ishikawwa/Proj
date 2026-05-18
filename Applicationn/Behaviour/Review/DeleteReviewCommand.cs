using Application.Utils;
using Domain.Constants;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Review
{
    public record DeleteReviewCommand : IRequest<ResponseContract<Unit>>
    {
        public Guid Id { get; set; }
        public bool IsArchive { get; set; }
    }

    public class DeleteReviewCommandValidator : AbstractValidator<DeleteReviewCommand>
    {
        public DeleteReviewCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id отзыва обязателен");
        }
    }

    public sealed class DeleteReviewCommandHandler(IReviewRepository reviewRepository) : IRequestHandler<DeleteReviewCommand, ResponseContract<Unit>>
    {
        public async Task<ResponseContract<Unit>> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
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

            await reviewRepository.DeleteAsync(request.Id);

            return new ResponseContract<Unit>(Unit.Value);
        }
    }
}