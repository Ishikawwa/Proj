using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Behaviour.User
{
    public record GetUsersQuery : IRequest<List<UserEntity>>
    {
        public string? NicknameFilter { get; set; }
    }

    public sealed class GetUsersQueryHandler(IUserRepository repository) : IRequestHandler<GetUsersQuery, List<UserEntity>>
    {
        public async Task<List<UserEntity>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            List<UserEntity> users = await repository.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(request.NicknameFilter))
                users = users.Where(x => x.Nickname.Contains(request.NicknameFilter, StringComparison.OrdinalIgnoreCase)).ToList();

            return users;
        }
    }
}