using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Behaviour.Institution
{
    public class CreateInstitutionCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public long Longitude { get; set; }
        public long Latitude { get; set; }
        public string Address { get; set; }
        public InstitutionTypeEnum Type { get; set; }
        public int? AveragePrice { get; set; }
        public string? WebUrl { get; set; }
    }

    public sealed class CreateInstitutionCommandHandler(IInstitutionRepository repository) : IRequestHandler<CreateInstitutionCommand, Guid>
    {
        public async Task<Guid> Handle(CreateInstitutionCommand request, CancellationToken cancellationToken)
        {
            InstitutionEntity entity = new()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Longitude = request.Longitude,
                Latitude = request.Latitude,
                Address = request.Address,
                Type = request.Type,
                AveragePrice = request.AveragePrice,
                WebUrl = request.WebUrl,
                Rating = 0,
                PhotoUrls = new List<string>(),
                Labels = new List<LabelTypeEnum>()
            };

            await repository.AddAsync(entity);

            return entity.Id;
        }
    }
}