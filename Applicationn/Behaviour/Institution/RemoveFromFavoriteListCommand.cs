using Application.Interfaces.Repositories;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Institution
{
    public record RemoveFromFavoriteListCommand : IRequest
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

    public sealed class RemoveFromFavoriteListCommandHandler(IFavouriteInstitutionRepository repository) : IRequestHandler<RemoveFromFavoriteListCommand>
    {
        public async Task Handle(RemoveFromFavoriteListCommand request, CancellationToken cancellationToken)
        {
            await repository.DeleteAsync(request.Id);
        }
    }
}