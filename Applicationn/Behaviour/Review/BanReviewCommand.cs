using FluentValidation;
using MediatR;

namespace Application.Behaviour.Review
{
    public record BanReviewCommand : IRequest
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

    public sealed class BanReviewCommandHandler(IReviewRepository repository) : IRequestHandler<BanReviewCommand>
    {
        public async Task Handle(BanReviewCommand request, CancellationToken cancellationToken)
        {
            await repository.BanAsync(request.Id);
        }
    }
}