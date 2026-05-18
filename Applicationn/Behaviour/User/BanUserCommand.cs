using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Constants;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.User
{
    public record BanUserCommand : IRequest<ResponseContract<Unit>>
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
    public sealed class BunUserCommandHandler(IUserRepository userRepository) : IRequestHandler<BanUserCommand, ResponseContract<Unit>>
    {
        public async Task<ResponseContract<Unit>> Handle(BanUserCommand request, CancellationToken cancellationToken)
        {
            UserEntity user = await userRepository.GetByIdAsync(request.Id);

            if (user == null)
            {
                return new ResponseContract<Unit>(ErrorCodes.UserNotFound);
            }

            if (user.IsBanned)
            {
                return new ResponseContract<Unit>(ErrorCodes.UserIsBanned);
            }
            await userRepository.BanAsync(request.Id);

            return new ResponseContract<Unit>(Unit.Value);
        }
    }
}