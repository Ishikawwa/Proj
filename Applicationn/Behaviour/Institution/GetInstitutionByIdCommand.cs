using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Institution
{
    public record GetInstitutionByIdCommand : IRequest<InstitutionEntity>
    {
        public Guid Id { get; set; }
    }

    public class GetInstitutionByIdCommandValidator : AbstractValidator<GetInstitutionByIdCommand>
    {
        public GetInstitutionByIdCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id заведения обязателен");
        }
    }

    public sealed class GetInstitutionByIdQueryHandler(IInstitutionRepository repository) : IRequestHandler<GetInstitutionByIdCommand, InstitutionEntity>
    {
        public async Task<InstitutionEntity> Handle(GetInstitutionByIdCommand request, CancellationToken cancellationToken)
        {
            return await repository.GetByIdAsync(request.Id);
        }
    }
}