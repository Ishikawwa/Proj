using Application.Utils;
using Domain.Constants;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Institution
{
    public record RemoveLabelFromInstitutionCommand : IRequest<ResponseContract<Unit>>
    {
        public Guid InstitutionId { get; set; }
        public LabelTypeEnum Label { get; set; }
    }

    public class RemoveLabelFromInstitutionCommandValidator : AbstractValidator<RemoveLabelFromInstitutionCommand>
    {
        public RemoveLabelFromInstitutionCommandValidator()
        {
            RuleFor(x => x.InstitutionId)
                .NotEmpty().WithMessage("InstitutionId обязателен");

            RuleFor(x => x.Label)
                .IsInEnum().WithMessage("Недопустимый тип лейбла");
        }
    }

    public sealed class RemoveLabelFromInstitutionCommandHandler(IInstitutionRepository institutionRepository) : IRequestHandler<RemoveLabelFromInstitutionCommand, ResponseContract<Unit>>
    {
        public async Task<ResponseContract<Unit>> Handle(RemoveLabelFromInstitutionCommand request, CancellationToken cancellationToken)
        {
            InstitutionEntity? institution = await institutionRepository.GetByIdAsync(request.InstitutionId);
            if (institution == null)
            {
                return new ResponseContract<Unit>(ErrorCodes.InstitutionNotFound);
            }

            institution.Labels.Remove(request.Label);
            await institutionRepository.UpdateAsync(institution);

            return new ResponseContract<Unit>(Unit.Value);
        }
    }
}