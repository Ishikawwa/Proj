using Application.Utils;
using Domain.Constants;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Institution
{
    public record AssignOwnerToInstitutionCommand : IRequest<ResponseContract<Unit>>
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

    public sealed class AssignOwnerToInstitutionCommandHandler(IInstitutionRepository institutionRepository) : IRequestHandler<AssignOwnerToInstitutionCommand, ResponseContract<Unit>>
    {
        public async Task<ResponseContract<Unit>> Handle(AssignOwnerToInstitutionCommand request, CancellationToken cancellationToken)
        {
            InstitutionEntity institution = await institutionRepository.GetByIdAsync(request.InstitutionId);

            if (institution == null)
            {
                return new ResponseContract<Unit>(ErrorCodes.InstitutionNotFound);
            }

            if (institution.OwnerId is not null)
            {
                return new ResponseContract<Unit>(ErrorCodes.OwnerAlreadyAssigned);
            }
            await institutionRepository.AssignOwnerAsync(request.InstitutionId, request.OwnerId);

            return new ResponseContract<Unit>(Unit.Value);
        }
    }
}