using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Review
{
    public record CreateReviewCommand : IRequest<ReviewEntity>
    {
        public Guid UserId { get; set; }
        public Guid InstitutionId { get; set; }
        public string Comment { get; set; }
        public int Score { get; set; }
    }
    public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
    {
        public CreateReviewCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId обязателен");

            RuleFor(x => x.InstitutionId)
                .NotEmpty().WithMessage("InstitutionId обязателен");

            RuleFor(x => x.Comment)
                .NotEmpty().WithMessage("Комментарий обязателен")
                .MaximumLength(10000).WithMessage("Комментарий не более 10.000 символов");

            RuleFor(x => x.Score)
                .InclusiveBetween(1, 5).WithMessage("Оценка должна быть от 1 до 5");
        }
    }

    public sealed class CreateReviewCommandHandler(IReviewRepository repository) : IRequestHandler<CreateReviewCommand, ReviewEntity>
    {
        public async Task<ReviewEntity> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            ReviewEntity entity = new()
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                InstitutionId = request.InstitutionId,
                Comment = request.Comment,
                Score = request.Score,
                CreatedAt = DateTime.UtcNow
            };

            await repository.AddAsync(entity);

            return entity;
        }
    }
}