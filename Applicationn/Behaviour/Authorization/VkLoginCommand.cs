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
        public string Code { get; set; } = string.Empty;
        public string DeviceId { get; set; } = string.Empty;
        public string CodeVerifier { get; set; } = string.Empty;
    }

    public class VkLoginCommandValidator : AbstractValidator<VkLoginCommand>
    {
        public VkLoginCommandValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Code обязателен");
            RuleFor(x => x.DeviceId)
                .NotEmpty().WithMessage("DeviceId обязателен");
            RuleFor(x => x.CodeVerifier)
                .NotEmpty().WithMessage("CodeVerifier обязателен");
        }
    }

    public sealed class VkLoginCommandHandler(
        IVkAuthService vkAuthService,
        IUserRepository userRepository,
        IJwtService jwtService) : IRequestHandler<VkLoginCommand, ResponseContract<string>>
    {
        public async Task<ResponseContract<string>> Handle(VkLoginCommand request, CancellationToken cancellationToken)
        {
            VkUserInfo? vkUser = await vkAuthService.ExchangeCodeAsync(request.Code, request.DeviceId, request.CodeVerifier);

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

            return new ResponseContract<string> { Data = jwt, Ok = true };
        }
    }
}