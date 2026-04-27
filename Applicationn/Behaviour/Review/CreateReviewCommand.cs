using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Behaviour.Review
{
    public class CreateReviewCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public Guid InstitutionId { get; set; }
        public string Comment { get; set; }
        public int Score { get; set; }
    }

    public sealed class CreateReviewCommandHandler(IReviewRepository repository) : IRequestHandler<CreateReviewCommand, Guid>
    {
        public async Task<Guid> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
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

            return entity.Id;
        }
    }
}