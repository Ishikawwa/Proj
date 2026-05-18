using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Constants;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Institution
{
    public record GetFavoriteListQuery : IRequest<ResponseContract<List<FavouriteInstitutionEntity>>>
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

    public sealed class GetFavoreListQueryHandler(
        IFavouriteInstitutionRepository repository,
        IUserRepository userRepository,
        IInstitutionRepository institutionRepository) : IRequestHandler<GetFavoriteListQuery, ResponseContract<List<FavouriteInstitutionEntity>>>
    {
        public async Task<ResponseContract<List<FavouriteInstitutionEntity>>> Handle(GetFavoriteListQuery request, CancellationToken cancellationToken)
        {
            UserEntity user = await userRepository.GetByIdAsync(request.UserId);
            InstitutionEntity institution = await institutionRepository.GetByIdAsync(request.UserId);
            if (user == null)
            {
                return new ResponseContract<List<FavouriteInstitutionEntity>>(ErrorCodes.UserNotFound);
            }

            if (institution == null)
            {
                return new ResponseContract<List<FavouriteInstitutionEntity>>(ErrorCodes.InstitutionNotFound);
            }

            await repository.GetByUserIdAsync(request.UserId);

            return new ResponseContract<List<FavouriteInstitutionEntity>>((List<FavouriteInstitutionEntity>)repository);
        }
    }
}