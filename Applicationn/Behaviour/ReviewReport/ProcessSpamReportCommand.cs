using Application.Interfaces.Repositories;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.ReviewReport
{
    public record ProcessSpamReportCommand : IRequest
    {
        public Guid Id { get; set; }
    }
    public class ProcessSpamReportCommandValidator : AbstractValidator<ProcessSpamReportCommand>
    {
        public ProcessSpamReportCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id жалобы обязателен");
        }
    }
    public sealed class ProcessSpamReportCommandHandler(ISpamReportRepository repository) : IRequestHandler<ProcessSpamReportCommand>
    {
        public Task Handle(ProcessSpamReportCommand request, CancellationToken cancellationToken)
            => repository.MarkAsProcessedAsync(request.Id);
    }
}