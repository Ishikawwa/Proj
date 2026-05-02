using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Behaviour.Institution
{
    public record AddLabelToInstitutionCommand : IRequest
    {
        public Guid InstitutionId { get; set; }
        public LabelTypeEnum Label { get; set; }
    }

    public sealed class AddLabelToInstitutionCommandHandler(IInstitutionRepository repository)
        : IRequestHandler<AddLabelToInstitutionCommand>
    {
        public async Task Handle(AddLabelToInstitutionCommand request, CancellationToken cancellationToken)
        {
            InstitutionEntity? institution = await repository.GetByIdAsync(request.InstitutionId);

            if (institution == null)
                throw new InvalidOperationException($"Заведение с Id={request.InstitutionId} не найдено.");

            if (!institution.Labels.Contains(request.Label))
                institution.Labels.Add(request.Label);

            await repository.UpdateAsync(institution);
        }
    }
}