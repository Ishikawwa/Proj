using Application.Interfaces.Repositories;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Institution
{
    public record GetFavoriteListQuery : IRequest<List<FavouriteInstitutionEntity>>
    {
        public Guid UserId { get; set; }
    }

    public class GetFavoriteListQueryValidator : AbstractValidator<GetFavoriteListQuery>
    {
        public GetFavoriteListQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId обязателен");
        }
    }

    public sealed class GetFavoreListQueryHandler(IFavouriteInstitutionRepository repository) : IRequestHandler<GetFavoriteListQuery, List<FavouriteInstitutionEntity>>
    {
        public async Task<List<FavouriteInstitutionEntity>> Handle(GetFavoriteListQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetByUserIdAsync(request.UserId);
        }
    }
}