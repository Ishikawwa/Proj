using Application.Interfaces.Repositories;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.User
{
    public record MuteUserCommand : IRequest
    {
        public Guid Id { get; set; }
    }
    public class MuteUserCommandValidator : AbstractValidator<MuteUserCommand>
    {
        public MuteUserCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id пользователя обязателен");
        }
    }

    public sealed class MuteUserCommandHandler(IUserRepository repository) : IRequestHandler<MuteUserCommand>
    {
        public async Task Handle(MuteUserCommand request, CancellationToken cancellationToken)
        {
            await repository.MuteAsync(request.Id);
        }
    }
}