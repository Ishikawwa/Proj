using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Behaviour.User
{
    public record BanUserCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public sealed class BunUserCommandHandler(IUserRepository repository) : IRequestHandler<BanUserCommand>
    {
        public async Task Handle(BanUserCommand request, CancellationToken cancellationToken)
        {
            await repository.BanAsync(request.Id);
        }
    }
}