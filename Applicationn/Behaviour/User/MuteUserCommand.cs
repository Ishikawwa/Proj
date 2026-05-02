using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Behaviour.User
{
    public record MuteUserCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public sealed class MuteUserCommandHandler(IUserRepository repository) : IRequestHandler<MuteUserCommand>
    {
        public async Task Handle(MuteUserCommand request, CancellationToken cancellationToken)
        {
            await repository.MuteAsync(request.Id);
        }
    }
}