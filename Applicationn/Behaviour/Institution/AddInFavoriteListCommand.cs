using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Behaviour.Institution
{
    public record AddInFavoriteListCommand : IRequest
    {
        public Guid UserId { get; set; }
        public Guid InstitutionId { get; set; }
    }

    public sealed class AddInFavoreListCommandHandler(IFavouriteInstitutionRepository repository) : IRequestHandler<AddInFavoriteListCommand>
    {
        public async Task Handle(AddInFavoriteListCommand request, CancellationToken cancellationToken)
        {
            FavouriteInstitutionEntity entity = new()
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                InstitutionId = request.InstitutionId
            };

            await repository.AddAsync(entity);

        }
    }
}