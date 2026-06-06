using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Constants;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Visiting
{
    public record CreateVisitingInstitutionCommand : IRequest<ResponseContract<VisitingEntity>>
    {
        public Guid UserId { get; set; }
        public Guid InstitutionId { get; set; }
    }
    public class CreateVisitingInstitutionCommandValidator : AbstractValidator<CreateVisitingInstitutionCommand>
    {
        public CreateVisitingInstitutionCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId обязателен");

            RuleFor(x => x.InstitutionId)
                .NotEmpty().WithMessage("InstitutionId обязателен");
        }
    }
    public sealed class CreateVisitingInstitutionCommandHandler(
        IVisitingRepository visitingRepository,
        IInstitutionRepository institutionRepository,
        IUserRepository userRepository) : IRequestHandler<CreateVisitingInstitutionCommand, ResponseContract<VisitingEntity>>
    {
        public async Task<ResponseContract<VisitingEntity>> Handle(CreateVisitingInstitutionCommand request, CancellationToken cancellationToken)
        {
            UserEntity user = await userRepository.GetByIdAsync(request.UserId);
            InstitutionEntity insitution = await institutionRepository.GetByIdAsync(request.InstitutionId);

            if (insitution == null)
            {
                return new ResponseContract<VisitingEntity>(ErrorCodes.InstitutionNotFound);
            }

            if (user == null)
            {
                return new ResponseContract<VisitingEntity>(ErrorCodes.UserNotFound);
            }

            VisitingEntity entity = new()
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                InstitutionId = request.InstitutionId
            };

            await visitingRepository.AddAsync(entity);

            return new ResponseContract<VisitingEntity>(entity);
        }
    }
}