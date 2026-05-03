using Application.Interfaces.Repositories;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Visiting
{
    public record CreateVisitingInstitutionCommand : IRequest<VisitingEntity>
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
    public sealed class CreateVisitingInstitutionCommandHandler(IVisitingRepository repository) : IRequestHandler<CreateVisitingInstitutionCommand, VisitingEntity>
    {
        public async Task<VisitingEntity> Handle(CreateVisitingInstitutionCommand request, CancellationToken cancellationToken)
        {
            VisitingEntity entity = new()
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                InstitutionId = request.InstitutionId
            };

            await repository.AddAsync(entity);

            return entity;
        }
    }
}