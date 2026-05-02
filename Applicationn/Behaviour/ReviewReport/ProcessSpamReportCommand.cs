using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Behaviour.ReviewReport
{
    public record ProcessSpamReportCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public sealed class ProcessSpamReportCommandHandler(ISpamReportRepository repository) : IRequestHandler<ProcessSpamReportCommand>
    {
        public Task Handle(ProcessSpamReportCommand request, CancellationToken cancellationToken)
            => repository.MarkAsProcessedAsync(request.Id);
    }
}