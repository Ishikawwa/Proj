using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Constants;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Institution
{
    public record AddInFavoriteListCommand : IRequest<ResponseContract<Unit>>
    {
        public Guid UserId { get; set; }
        public Guid InstitutionId { get; set; }
    }

    public class AddInFavoriteListCommandValidator : AbstractValidator<AddInFavoriteListCommand>
    {
        public AddInFavoriteListCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId обязателен");

            RuleFor(x => x.InstitutionId)
                .NotEmpty().WithMessage("InstitutionId обязателен");
        }
    }

    public sealed class AddInFavoreListCommandHandler(
        IFavouriteInstitutionRepository favouriteInstitutionRepository,
        IUserRepository userRepository,
        IInstitutionRepository institutionRepository) : IRequestHandler<AddInFavoriteListCommand, ResponseContract<Unit>>
    {
        public async Task<ResponseContract<Unit>> Handle(AddInFavoriteListCommand request, CancellationToken cancellationToken)
        {
            InstitutionEntity institution = await institutionRepository.GetByIdAsync(request.InstitutionId);
            UserEntity user = await userRepository.GetByIdAsync(request.UserId);

            if (institution == null)
            {
                return new ResponseContract<Unit>(ErrorCodes.InstitutionNotFound);
            }

            if (user == null)
            {
                return new ResponseContract<Unit>(ErrorCodes.UserNotFound);
            }

            FavouriteInstitutionEntity entity = new()
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                InstitutionId = request.InstitutionId
            };

            await favouriteInstitutionRepository.AddAsync(entity);

            return new ResponseContract<Unit>(Unit.Value);
        }
    }
}