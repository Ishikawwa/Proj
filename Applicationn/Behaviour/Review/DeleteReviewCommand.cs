using FluentValidation;
using MediatR;

namespace Application.Behaviour.Review
{
    public record DeleteReviewCommand : IRequest
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

    public sealed class DeleteReviewCommandHandler(IReviewRepository repository) : IRequestHandler<DeleteReviewCommand>
    {
        public async Task Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            await repository.DeleteAsync(request.Id);
        }
    }
}