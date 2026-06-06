using Application.Utils;
using Domain.Constants;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Institution
{
    public record DeleteInstitutionCommand : IRequest<ResponseContract<Unit>>
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

    public sealed class DeleteInstitutionCommandHandler(IInstitutionRepository repository) : IRequestHandler<DeleteInstitutionCommand, ResponseContract<Unit>>
    {
        public async Task<ResponseContract<Unit>> Handle(DeleteInstitutionCommand request, CancellationToken cancellationToken)
        {
            InstitutionEntity institution = await repository.GetByIdAsync(request.Id);
            if (institution == null)
            {
                return new ResponseContract<Unit>(ErrorCodes.InstitutionNotFound);
            }

            await repository.DeleteAsync(request.Id);

            return new ResponseContract<Unit>(Unit.Value);
        }
    }
}