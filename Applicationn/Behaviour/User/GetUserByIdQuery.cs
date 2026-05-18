using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Constants;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.User
{
    public record GetUserByIdQuery : IRequest<ResponseContract<UserEntity>>
    {
        public Guid Id { get; set; }
    }

    public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id пользователя обязателен");
        }
    }
    public sealed class GetUserByIdQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserByIdQuery, ResponseContract<UserEntity>>
    {
        public async Task<ResponseContract<UserEntity>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            UserEntity user = await userRepository.GetByIdAsync(request.Id);
            if (user == null)
            {
                return new ResponseContract<UserEntity>(ErrorCodes.UserNotFound);
            }

            await userRepository.GetByIdAsync(request.Id);

            return new ResponseContract<UserEntity>(user);
        }
    }
}