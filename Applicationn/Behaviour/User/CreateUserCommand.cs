using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.User
{
    public record CreateUserCommand : IRequest<ResponseContract<UserEntity>>
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; }
        public string? AvatarUrl { get; set; }
    }

    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Nickname)
                .NotEmpty().WithMessage("Никнейм обязателен")
                .MinimumLength(2).WithMessage("Никнейм минимум 2 символа")
                .MaximumLength(50).WithMessage("Никнейм не более 50 символов");

            RuleFor(x => x.AvatarUrl)
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
                .When(x => !string.IsNullOrEmpty(x.AvatarUrl))
                .WithMessage("AvatarUrl некорректный");
        }
    }

    public sealed class CreateUserCommandHandler(IUserRepository userRepository) : IRequestHandler<CreateUserCommand, ResponseContract<UserEntity>>
    {
        public async Task<ResponseContract<UserEntity>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            UserEntity entity = new()
            {
                Id = Guid.NewGuid(),
                Nickname = request.Nickname,
                AvatarUrl = request.AvatarUrl,
                CreatedAt = DateTime.UtcNow
            };

            await userRepository.AddAsync(entity);

            return new ResponseContract<UserEntity>(entity);
        }
    }
}