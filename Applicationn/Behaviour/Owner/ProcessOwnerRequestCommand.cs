using Application.Interfaces.Repositories;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.OwnerRequest
{
    public record ProcessOwnerRequestCommand : IRequest
    {
        public Guid Id { get; set; }
    }
    public class ProcessOwnerRequestCommandValidator : AbstractValidator<ProcessOwnerRequestCommand>
    {
        public ProcessOwnerRequestCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id запроса обязателен");
        }
    }
    public sealed class ProcessOwnerRequestCommandHandler(IOwnerRequestRepository repository) : IRequestHandler<ProcessOwnerRequestCommand>
    {
        public Task Handle(ProcessOwnerRequestCommand request, CancellationToken cancellationToken)
            => repository.MarkAsProcessedAsync(request.Id);
    }
}