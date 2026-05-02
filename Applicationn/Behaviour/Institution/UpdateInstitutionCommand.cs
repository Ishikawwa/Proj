using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Behaviour.Institution
{
    public record UpdateInstitutionCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public InstitutionTypeEnum Type { get; set; }
        public int? AveragePrice { get; set; }
        public string? WebUrl { get; set; }
    }

    public sealed class UpdateInstitutionCommandHandler(IInstitutionRepository repository) : IRequestHandler<UpdateInstitutionCommand>
    {
        public async Task Handle(UpdateInstitutionCommand request, CancellationToken cancellationToken)
        {
            InstitutionEntity? entity = await repository.GetByIdAsync(request.Id);
            if (entity == null)
                throw new InvalidOperationException($"Заведение с Id={request.Id} не найдено");

            entity.Name = request.Name;
            entity.Address = request.Address;
            entity.Type = request.Type;
            entity.AveragePrice = request.AveragePrice;
            entity.WebUrl = request.WebUrl;

            await repository.UpdateAsync(entity);
        }
    }
}