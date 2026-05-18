using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Institution
{
    public record GetInstitutionByOwnerIdQuery : IRequest<ResponseContract<List<InstitutionEntity>>>
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

    public sealed class GetInstitutionByOwnerIdQueryHandler(IInstitutionRepository institutionRepository, IOwnerRequestRepository ownerRequestRepository) : IRequestHandler<GetInstitutionByOwnerIdQuery, ResponseContract<List<InstitutionEntity>>>
    {
        public async Task<ResponseContract<List<InstitutionEntity>>> Handle(GetInstitutionByOwnerIdQuery request, CancellationToken cancellationToken)
        {
            await institutionRepository.GetByOwnerIdAsync(request.OwnerId);

            return new ResponseContract<List<InstitutionEntity>>((List<InstitutionEntity>)institutionRepository);
        }
    }
}