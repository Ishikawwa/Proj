using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Behaviour.ReviewReport
{
    public record GetReviewReportQuery : IRequest<List<SpamReportEntity>>
    {
    }

    public sealed class GetReviewReportQueryHandler(ISpamReportRepository repository) : IRequestHandler<GetReviewReportQuery, List<SpamReportEntity>>
    {
        public async Task<List<SpamReportEntity>> Handle(GetReviewReportQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetPendingAsync();
        }
    }
}