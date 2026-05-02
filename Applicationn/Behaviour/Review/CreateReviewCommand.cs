using Domain.Entities;
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