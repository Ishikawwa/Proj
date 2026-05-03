using Application.Interfaces.Repositories;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.OwnerRequest
{
    public record CreateOwnerRequestCommand : IRequest<OwnerRequestEntity>
    {
        public Guid UserId { get; set; }
        public Guid InstitutionId { get; set; }
        public string Comment { get; set; }
    }
    public class CreateOwnerRequestCommandValidator : AbstractValidator<CreateOwnerRequestCommand>
    {
        public CreateOwnerRequestCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId обязателен");

            RuleFor(x => x.InstitutionId)
                .NotEmpty().WithMessage("InstitutionId обязателен");

            RuleFor(x => x.Comment)
                .NotEmpty().WithMessage("Комментарий обязателен")
                .MaximumLength(10000).WithMessage("Комментарий не более 10.000 символов");
        }
    }

    public sealed class CreateOwnerRequestCommandHandler(IOwnerRequestRepository repository) : IRequestHandler<CreateOwnerRequestCommand, OwnerRequestEntity>
    {
        public async Task<OwnerRequestEntity> Handle(CreateOwnerRequestCommand request, CancellationToken cancellationToken)
        {
            OwnerRequestEntity entity = new()
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                InstitutionId = request.InstitutionId,
                Comment = request.Comment,
            };

            await repository.AddAsync(entity);

            return entity;
        }
    }
}