using Application.Utils;
using Domain.Constants;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Review
{
    public record UpdateReviewCommand : IRequest<ResponseContract<Unit>>
    {
        public Guid Id { get; set; }
        public string Comment { get; set; }
        public int Score { get; set; }
    }

    public class UpdateReviewCommandValidator : AbstractValidator<UpdateReviewCommand>
    {
        public UpdateReviewCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id отзыва обязателен");

            RuleFor(x => x.Comment)
                .NotEmpty().WithMessage("Комментарий обязателен")
                .MaximumLength(10000).WithMessage("Комментарий не более 10.000 символов");

            RuleFor(x => x.Score)
                .InclusiveBetween(1, 5).WithMessage("Оценка должна быть от 1 до 5");
        }
    }

    public sealed class UpdateReviewCommandHandler(IReviewRepository repository) : IRequestHandler<UpdateReviewCommand, ResponseContract<Unit>>
    {
        public async Task<ResponseContract<Unit>> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            ReviewEntity? review = await repository.GetByIdAsync(request.Id);
            if (review == null)
            {
                return new ResponseContract<Unit>(ErrorCodes.ReviewNotFound);
            }

            review.Comment = request.Comment;
            review.Score = request.Score;

            await repository.UpdateAsync(review);

            return new ResponseContract<Unit>(Unit.Value);
        }
    }
}