using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using MediatR;

namespace Application.Behaviour.ReviewReport
{
    public record GetReviewReportQuery : IRequest<ResponseContract<List<SpamReportEntity>>>
    {
    }

    public sealed class GetReviewReportQueryHandler(ISpamReportRepository repository) : IRequestHandler<GetReviewReportQuery, ResponseContract<List<SpamReportEntity>>>
    {
        public async Task<ResponseContract<List<SpamReportEntity>>> Handle(GetReviewReportQuery request, CancellationToken cancellationToken)
        {
            List<SpamReportEntity> reports = await repository.GetPendingAsync();

            return new ResponseContract<List<SpamReportEntity>>(reports);
        }
    }
}