using Domain.Entities;
using MediatR;

namespace Application.Behaviour.Review
{
    public record UpdateReviewCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Comment { get; set; }
        public int Score { get; set; }
    }

    public sealed class UpdateReviewCommandHandler(IReviewRepository repository) : IRequestHandler<UpdateReviewCommand>
    {
        public async Task Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            ReviewEntity? entity = await repository.GetByIdAsync(request.Id);
            if (entity == null)
                throw new InvalidOperationException($"Отзыв с Id={request.Id} не найден.");

            entity.Comment = request.Comment;
            entity.Score = request.Score;

            await repository.UpdateAsync(entity);
        }
    }
}