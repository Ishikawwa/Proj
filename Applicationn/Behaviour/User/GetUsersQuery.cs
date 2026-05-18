using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using MediatR;

namespace Application.Behaviour.User
{
    public record GetUsersQuery : IRequest<ResponseContract<List<UserEntity>>>
    {
        public string? NicknameFilter { get; set; }
    }

    public sealed class GetUsersQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUsersQuery, ResponseContract<List<UserEntity>>>
    {
        public async Task<ResponseContract<List<UserEntity>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            List<UserEntity> users = await userRepository.GetAllAsync();


            if (!string.IsNullOrWhiteSpace(request.NicknameFilter))
                users = users.Where(x => x.Nickname.Contains(request.NicknameFilter, StringComparison.OrdinalIgnoreCase)).ToList();

            return new ResponseContract<List<UserEntity>>(users);
        }
    }
}