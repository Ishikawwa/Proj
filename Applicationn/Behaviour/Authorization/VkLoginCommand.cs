using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Behaviour.Auth
{
    public record VkLoginCommand : IRequest<ResponseContract<string>>
    {
        public string SilentToken { get; set; } = string.Empty;
        public string Uuid { get; set; } = string.Empty;
    }

    public class VkLoginCommandValidator : AbstractValidator<VkLoginCommand>
    {
        public VkLoginCommandValidator()
        {
            RuleFor(x => x.SilentToken)
                .NotEmpty().WithMessage("SilentToken обязателен");

            RuleFor(x => x.Uuid)
                .NotEmpty().WithMessage("Uuid обязателен");
        }
    }

    public sealed class VkLoginCommandHandler(
        IVkAuthService vkAuthService,
        IUserRepository userRepository,
        IJwtService jwtService) : IRequestHandler<VkLoginCommand, ResponseContract<string>>
    {
        public async Task<ResponseContract<string>> Handle(VkLoginCommand request, CancellationToken cancellationToken)
        {
            VkUserInfo? vkUser = await vkAuthService.ExchangeSilentTokenAsync(request.SilentToken, request.Uuid);

            if (vkUser == null)
                return new ResponseContract<string>("VkTokenInvalid");

            UserEntity? user = await userRepository.GetByVkIdAsync(vkUser.VkId);

            if (user == null)
            {
                user = new UserEntity
                {
                    Id = Guid.NewGuid(),
                    VkId = vkUser.VkId,
                    Nickname = $"{vkUser.FirstName} {vkUser.LastName}".Trim(),
                    AvatarUrl = vkUser.Photo,
                    Email = vkUser.Email,
                    CreatedAt = DateTime.UtcNow
                };

                await userRepository.AddAsync(user);
            }

            string jwt = jwtService.GenerateToken(user);
            return new ResponseContract<string>(jwt);
        }
    }
}