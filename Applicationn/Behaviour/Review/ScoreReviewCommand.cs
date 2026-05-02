using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Behaviour.Review
{
    public record ScoreReviewCommand : IRequest<ReviewScoreEntity>
    {
        public Guid ReviewId { get; set; }
        public Guid UserId { get; set; }
        public bool IsLiked { get; set; }
    }

    public sealed class ScoreReviewCommandHandler(IReviewScoreRepository repository) : IRequestHandler<ScoreReviewCommand, ReviewScoreEntity>
    {
        public async Task<ReviewScoreEntity> Handle(ScoreReviewCommand request, CancellationToken cancellationToken)
        {
            ReviewScoreEntity entity = new()
            {
                Id = Guid.NewGuid(),
                ReviewId = request.ReviewId,
                UserId = request.UserId,
                IsLiked = request.IsLiked
            };

            await repository.AddAsync(entity);

            return entity;
        }
    }
}