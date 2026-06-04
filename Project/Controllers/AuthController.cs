using Application.Behaviour.Auth;
using Application.Behaviour.Institution;
using Application.Utils;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.DTO.AuthDto;
using Project.DTO.UserDto;
using System.Security.Claims;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        [HttpPost("vk")]
        public async Task<ResponseContract<AuthResponseDto>> VkLogin([FromBody] VkLoginDto dto)
        {
            ResponseContract<string> result =
                await mediator.Send(dto.Adapt<VkLoginCommand>());

            if (!result.Ok)
                return new ResponseContract<AuthResponseDto>(result.ErrorCode!);

            return new ResponseContract<AuthResponseDto>(
                new AuthResponseDto { Token = result.Data! });
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<ResponseContract<UserDto>> GetMe()
        {
            var vkId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(vkId))
                return new ResponseContract<UserDto>("Unauthorized");

            return (await mediator.Send(new GetMeQuery(vkId))).Adapt<ResponseContract<UserDto>>();
        }
    }
}