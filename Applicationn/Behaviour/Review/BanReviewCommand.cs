using MediatR;

namespace Application.Behaviour.Review
{
    public record BanReviewCommand : IRequest
    {
        public Guid Id { get; set; }
        public bool IsBanned { get; set; }
    }

    public sealed class BanReviewCommandHandler(IReviewRepository repository) : IRequestHandler<BanReviewCommand>
    {
        public async Task Handle(BanReviewCommand request, CancellationToken cancellationToken)
        {
            await repository.BanAsync(request.Id);
        }
    }
}