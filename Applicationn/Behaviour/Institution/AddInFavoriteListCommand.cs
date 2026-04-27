using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Behaviour.Institution
{
    public class AddInFavoriteListCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public Guid InstitutionId { get; set; }
    }

    public sealed class AddInFavoreListCommandHandler(IFavouriteInstitutionRepository repository) : IRequestHandler<AddInFavoriteListCommand, Guid>
    {
        public async Task<Guid> Handle(AddInFavoriteListCommand request, CancellationToken cancellationToken)
        {
            FavouriteInstitutionEntity entity = new()
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                InstitutionId = request.InstitutionId
            };

            await repository.AddAsync(entity);

            return entity.Id;
        }
    }
}