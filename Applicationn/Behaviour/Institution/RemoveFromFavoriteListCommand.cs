using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Behaviour.Institution
{
    public record RemoveFromFavoriteListCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public sealed class RemoveFromFavoriteListCommandHandler(IFavouriteInstitutionRepository repository) : IRequestHandler<RemoveFromFavoriteListCommand>
    {
        public async Task Handle(RemoveFromFavoriteListCommand request, CancellationToken cancellationToken)
        {
            await repository.DeleteAsync(request.Id);
        }
    }
}