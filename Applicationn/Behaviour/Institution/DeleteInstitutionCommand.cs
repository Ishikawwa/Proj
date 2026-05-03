using FluentValidation;
using MediatR;

namespace Application.Behaviour.Institution
{
    public record DeleteInstitutionCommand : IRequest
    {
        public Guid Id { get; set; }
        public bool IsArchive { get; set; }
    }

    public class DeleteInstitutionCommandValidator : AbstractValidator<DeleteInstitutionCommand>
    {
        public DeleteInstitutionCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id заведения обязателен");
        }
    }

    public sealed class DeleteInstitutionCommandHandler(IInstitutionRepository repository) : IRequestHandler<DeleteInstitutionCommand>
    {
        public async Task Handle(DeleteInstitutionCommand request, CancellationToken cancellationToken)
        {
            await repository.DeleteAsync(request.Id);
        }
    }
}