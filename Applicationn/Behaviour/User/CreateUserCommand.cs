using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Behaviour.User
{
    public record CreateUserCommand : IRequest<UserEntity>
    {
        public string Nickname { get; set; }
        public string? AvatarUrl { get; set; }
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