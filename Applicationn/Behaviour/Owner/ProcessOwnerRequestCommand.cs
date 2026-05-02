using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Behaviour.OwnerRequest
{
    public record ProcessOwnerRequestCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public sealed class ProcessOwnerRequestCommandHandler(IOwnerRequestRepository repository) : IRequestHandler<ProcessOwnerRequestCommand>
    {
        public Task Handle(ProcessOwnerRequestCommand request, CancellationToken cancellationToken)
            => repository.MarkAsProcessedAsync(request.Id);
    }
}