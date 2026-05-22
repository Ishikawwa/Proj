using Application.Interfaces.Repositories;
using Application.Utils;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.OwnerRequest
{
    public record ProcessOwnerRequestCommand : IRequest<ResponseContract<Unit>>
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
    public sealed class ProcessOwnerRequestCommandHandler(IOwnerRequestRepository repository) : IRequestHandler<ProcessOwnerRequestCommand, ResponseContract<Unit>>
    {
        public async Task<ResponseContract<Unit>> Handle(ProcessOwnerRequestCommand request, CancellationToken cancellationToken)
        {
            await repository.MarkAsProcessedAsync(request.Id);

            return new ResponseContract<Unit>(Unit.Value);
        }
    }
}