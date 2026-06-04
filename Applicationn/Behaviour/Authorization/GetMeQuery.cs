using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Constants;
using Domain.Entities;
using MediatR;

namespace Application.Behaviour.Institution
{
    public record GetMeQuery(string vkId) : IRequest<ResponseContract<UserEntity>>;

    public sealed class GetMeQueryHandler(IUserRepository userRepository) : IRequestHandler<GetMeQuery, ResponseContract<UserEntity>>
    {
        public async Task<ResponseContract<UserEntity>> Handle(GetMeQuery request, CancellationToken cancellationToken)
        {
            UserEntity? user = await userRepository.GetByVkIdAsync(request.vkId);

            if (user == null)
                return new ResponseContract<UserEntity>(ErrorCodes.UserNotFound);

            return new ResponseContract<UserEntity>(user);
        }
    }
}