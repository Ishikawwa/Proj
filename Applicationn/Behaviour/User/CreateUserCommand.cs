using Application.Interfaces.Repositories;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.User
{
    public record CreateUserCommand : IRequest<UserEntity>
    {
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

    public sealed class CreateUserCommandHandler(IUserRepository repository) : IRequestHandler<CreateUserCommand, UserEntity>
    {
        public async Task<UserEntity> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            UserEntity entity = new()
            {
                Id = Guid.NewGuid(),
                Nickname = request.Nickname,
                AvatarUrl = request.AvatarUrl,
                CreatedAt = DateTime.UtcNow
            };

            await repository.AddAsync(entity);

            return entity;
        }
    }
}