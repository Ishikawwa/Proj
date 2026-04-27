using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Behaviour.Review
{
    public class ScoreReviewCommand : IRequest<Guid>
    {
        public Guid ReviewId { get; set; }
        public Guid UserId { get; set; }
        public bool IsLiked { get; set; }
    }

    public sealed class ScoreReviewCommandHandler(IReviewScoreRepository repository) : IRequestHandler<ScoreReviewCommand, Guid>
    {
        public async Task<Guid> Handle(ScoreReviewCommand request, CancellationToken cancellationToken)
        {
            ReviewScoreEntity entity = new()
            {
                Id = Guid.NewGuid(),
                ReviewId = request.ReviewId,
                UserId = request.UserId,
                isLiked = request.IsLiked
            };

            await repository.AddAsync(entity);

            return entity.Id;
        }
    }
}