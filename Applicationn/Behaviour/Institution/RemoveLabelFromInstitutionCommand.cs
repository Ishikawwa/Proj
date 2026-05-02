using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Behaviour.Institution
{
    public record RemoveLabelFromInstitutionCommand : IRequest
    {
        public Guid InstitutionId { get; set; }
        public LabelTypeEnum Label { get; set; }
    }

    public sealed class RemoveLabelFromInstitutionCommandHandler(IInstitutionRepository repository)
        : IRequestHandler<RemoveLabelFromInstitutionCommand>
    {
        public async Task Handle(RemoveLabelFromInstitutionCommand request, CancellationToken cancellationToken)
        {
            InstitutionEntity? institution = await repository.GetByIdAsync(request.InstitutionId);
            if (institution == null)
                throw new InvalidOperationException($"Заведение с Id={request.InstitutionId} не найдено.");

            institution.Labels.Remove(request.Label);
            await repository.UpdateAsync(institution);
        }
    }
}