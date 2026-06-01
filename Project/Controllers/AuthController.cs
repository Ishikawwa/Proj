using Application.Behaviour.Auth;
using Application.Behaviour.Institution;
using Application.Utils;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.DTO.AuthDto;
using Project.DTO.UserDto;

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
            var userId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);

            return (await mediator.Send(new GetMeQuery(userId))).Adapt<ResponseContract<UserDto>>();
        }
    }
}