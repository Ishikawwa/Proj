using Application.Utils;
using Domain.Constants;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Institution
{
    public record AddLabelToInstitutionCommand : IRequest<ResponseContract<Unit>>
    {
        public Guid InstitutionId { get; set; }
        public LabelTypeEnum Label { get; set; }
    }

    public class AddLabelToInstitutionCommandValidator : AbstractValidator<AddLabelToInstitutionCommand>
    {
        public AddLabelToInstitutionCommandValidator()
        {
            RuleFor(x => x.InstitutionId)
                .NotEmpty().WithMessage("InstitutionId обязателен");

            RuleFor(x => x.Label)
                .IsInEnum().WithMessage("Недопустимый тип лейбла");
        }
    }

    public sealed class AddLabelToInstitutionCommandHandler(IInstitutionRepository repository) : IRequestHandler<AddLabelToInstitutionCommand, ResponseContract<Unit>>
    {
        public async Task<ResponseContract<Unit>> Handle(AddLabelToInstitutionCommand request, CancellationToken cancellationToken)
        {
            InstitutionEntity? institution = await repository.GetByIdAsync(request.InstitutionId);

            if (institution == null)
            {
                return new ResponseContract<Unit>(ErrorCodes.InstitutionNotFound);
            }

            if (!institution.Labels.Contains(request.Label))
                institution.Labels.Add(request.Label);

            await repository.UpdateAsync(institution);

            return new ResponseContract<Unit>(Unit.Value);
        }
    }
}