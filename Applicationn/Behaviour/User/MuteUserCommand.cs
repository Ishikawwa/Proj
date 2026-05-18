using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Constants;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.User
{
    public record MuteUserCommand : IRequest<ResponseContract<Unit>>
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

    public sealed class MuteUserCommandHandler(IUserRepository userRepository) : IRequestHandler<MuteUserCommand, ResponseContract<Unit>>
    {
        public async Task<ResponseContract<Unit>> Handle(MuteUserCommand request, CancellationToken cancellationToken)
        {
            UserEntity user = await userRepository.GetByIdAsync(request.Id);

            if (user == null)
            {
                return new ResponseContract<Unit>(ErrorCodes.UserNotFound);
            }

            if (user.IsMuted)
            {
                return new ResponseContract<Unit>(ErrorCodes.UserIsMuted);
            }

            if (user.IsBanned)
            {
                return new ResponseContract<Unit>(ErrorCodes.UserIsBanned);
            }

            await userRepository.MuteAsync(request.Id);

            return new ResponseContract<Unit>(Unit.Value);
        }
    }
}