using FluentValidation;
using MediatR;

namespace Application.Behaviour.Institution
{
    public record AssignOwnerToInstitutionCommand : IRequest
    {
        public Guid InstitutionId { get; set; }
        public Guid OwnerId { get; set; }
    }

    public class AssignOwnerToInstitutionCommandValidator : AbstractValidator<AssignOwnerToInstitutionCommand>
    {
        public AssignOwnerToInstitutionCommandValidator()
        {
            RuleFor(x => x.InstitutionId)
                .NotEmpty().WithMessage("InstitutionId обязателен");

            RuleFor(x => x.OwnerId)
                .NotEmpty().WithMessage("OwnerId обязателен");
        }
    }

    public sealed class AssignOwnerToInstitutionCommandHandler(IInstitutionRepository repository) : IRequestHandler<AssignOwnerToInstitutionCommand>
    {
        public async Task Handle(AssignOwnerToInstitutionCommand request, CancellationToken cancellationToken)
        {
            await repository.AssignOwnerAsync(request.InstitutionId, request.OwnerId);
        }
    }
}