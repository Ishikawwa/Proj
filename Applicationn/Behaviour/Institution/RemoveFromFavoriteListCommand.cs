using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Constants;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Institution
{
    public record RemoveFromFavoriteListCommand : IRequest<ResponseContract<Unit>>
    {
        public Guid Id { get; set; }
    }

    public class RemoveFromFavoriteListCommandValidator : AbstractValidator<RemoveFromFavoriteListCommand>
    {
        public RemoveFromFavoriteListCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id обязателен");
        }
    }

    public sealed class RemoveFromFavoriteListCommandHandler(IFavouriteInstitutionRepository institutionRepository, IUserRepository userRepository) : IRequestHandler<RemoveFromFavoriteListCommand, ResponseContract<Unit>>
    {
        public async Task<ResponseContract<Unit>> Handle(RemoveFromFavoriteListCommand request, CancellationToken cancellationToken)
        {
            List<FavouriteInstitutionEntity> institution = await institutionRepository.GetByUserIdAsync(request.Id);
            UserEntity user = await userRepository.GetByIdAsync(request.Id);

            if (user == null)
            {
                return new ResponseContract<Unit>(ErrorCodes.UserNotFound);
            }

            if (institution == null)
            {
                return new ResponseContract<Unit>(ErrorCodes.InstitutionNotFound);
            }
            await institutionRepository.DeleteAsync(request.Id);

            return new ResponseContract<Unit>(Unit.Value);
        }
    }
}