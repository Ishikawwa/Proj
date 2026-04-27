using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Behaviour.User
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public string Nickname { get; set; }
        public string AvatarUrl { get; set; }
    }

    public sealed class CreateUserCommandHandler(IUserRepository repository) : IRequestHandler<CreateUserCommand, Guid>
    {
        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            UserEntity entity = new()
            {
                Id = Guid.NewGuid(),
                Nickname = request.Nickname,
                AvatarUrl = request.AvatarUrl,
                CreatedAt = DateTime.UtcNow
            };

            await repository.AddAsync(entity);

            return entity.Id;
        }
    }
}