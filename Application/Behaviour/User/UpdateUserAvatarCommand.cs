using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Constants;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.User
{
    public record UpdateUserAvatarCommand : IRequest<ResponseContract<UserEntity>>
    {
        public Guid Id { get; set; }
        public string? AvatarUrl { get; set; }
    }

    public class UpdateUserAvatarCommandValidator : AbstractValidator<UpdateUserAvatarCommand>
    {
        public UpdateUserAvatarCommandValidator()
        {
            RuleFor(x => x.AvatarUrl)
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
                .When(x => !string.IsNullOrEmpty(x.AvatarUrl))
                .WithMessage("AvatarUrl некорректный");
        }
    }

    public sealed class UpdateUserAvatarCommandHandler(IUserRepository userRepository) : IRequestHandler<UpdateUserAvatarCommand, ResponseContract<UserEntity>>
    {
        public async Task<ResponseContract<UserEntity>> Handle(UpdateUserAvatarCommand request, CancellationToken cancellationToken)
        {
            UserEntity? user = await userRepository.GetByIdAsync(request.Id);
            if (user == null)
                return new ResponseContract<UserEntity>(ErrorCodes.UserNotFound);

            user.AvatarUrl = request.AvatarUrl;
            await userRepository.UpdateAsync(user);
            return new ResponseContract<UserEntity>(user);
        }
    }
}