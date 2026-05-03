using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Institution
{
    public record GetInstitutionByOwnerIdQuery : IRequest<List<InstitutionEntity>>
    {
        public Guid OwnerId { get; set; }
    }

    public class GetInstitutionByOwnerIdQueryValidator : AbstractValidator<GetInstitutionByOwnerIdQuery>
    {
        public GetInstitutionByOwnerIdQueryValidator()
        {
            RuleFor(x => x.OwnerId)
                .NotEmpty().WithMessage("OwnerId обязателен");
        }
    }

    public sealed class GetInstitutionByOwnerIdQueryHandler(IInstitutionRepository repository) : IRequestHandler<GetInstitutionByOwnerIdQuery, List<InstitutionEntity>>
    {
        public async Task<List<InstitutionEntity>> Handle(GetInstitutionByOwnerIdQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetByOwnerIdAsync(request.OwnerId);
        }
    }
}