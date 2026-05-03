using Application.Interfaces.Repositories;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.ReviewReport
{
    public record CreateReviewReportCommand : IRequest<SpamReportEntity>
    {
        public Guid UserId { get; set; }
        public Guid ReviewId { get; set; }
        public string Comment { get; set; }
    }
    public class CreateReviewReportCommandValidator : AbstractValidator<CreateReviewReportCommand>
    {
        public CreateReviewReportCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId обязателен");

            RuleFor(x => x.ReviewId)
                .NotEmpty().WithMessage("ReviewId обязателен");

            RuleFor(x => x.Comment)
                .NotEmpty().WithMessage("Комментарий обязателен")
                .MaximumLength(10000).WithMessage("Комментарий не более 10.000 символов");
        }
    }

    public sealed class CreateReviewReportCommandHandler(ISpamReportRepository repository, IReviewRepository reviewRepository) : IRequestHandler<CreateReviewReportCommand, SpamReportEntity>
    {
        public async Task<SpamReportEntity> Handle(CreateReviewReportCommand request, CancellationToken cancellationToken)
        {
            ReviewEntity? review = await reviewRepository.GetByIdAsync(request.ReviewId);
            if (review is null)
                throw new InvalidOperationException($"Отзыв с Id={request.ReviewId} не найден");

            SpamReportEntity entity = new()
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                ReviewId = request.ReviewId,
                Comment = request.Comment,
            };

            await repository.AddAsync(entity);

            return entity;
        }
    }
}