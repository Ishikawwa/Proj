using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Behaviour.ReviewReport
{
    public class CreateReviewReportCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public Guid ReviewId { get; set; }
        public string Comment { get; set; }
    }

    public sealed class CreateReviewReportCommandHandler(ISpamReportRepository repository) : IRequestHandler<CreateReviewReportCommand, Guid>
    {
        public async Task<Guid> Handle(CreateReviewReportCommand request, CancellationToken cancellationToken)
        {
            SpamReportEntity entity = new()
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                ReviewId = request.ReviewId,
                Comment = request.Comment
            };

            await repository.AddAsync(entity);

            return entity.Id;
        }
    }
}