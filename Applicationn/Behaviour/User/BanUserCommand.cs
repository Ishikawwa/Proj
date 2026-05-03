using Application.Interfaces.Repositories;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.User
{
    public record BanUserCommand : IRequest
    {
        public Guid Id { get; set; }
    }
    public class BanUserCommandValidator : AbstractValidator<BanUserCommand>
    {
        public BanUserCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id пользователя обязателен");
        }
    }
    public sealed class BunUserCommandHandler(IUserRepository repository) : IRequestHandler<BanUserCommand>
    {
        public async Task Handle(BanUserCommand request, CancellationToken cancellationToken)
        {
            await repository.BanAsync(request.Id);
        }
    }
}