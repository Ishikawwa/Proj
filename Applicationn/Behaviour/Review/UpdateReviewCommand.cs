using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Behaviour.Review
{
    public class UpdateReviewCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Comment { get; set; }
        public int Score { get; set; }
    }

    public sealed class UpdateReviewCommandHandler(IReviewRepository repository) : IRequestHandler<UpdateReviewCommand>
    {
        public async Task Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            ReviewEntity entity = await repository.GetByIdAsync(request.Id);

            entity.Comment = request.Comment;
            entity.Score = request.Score;

            await repository.UpdateAsync(entity);
        }
    }
}