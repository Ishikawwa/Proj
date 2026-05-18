using Application.Behaviour.User;
using Application.Utils;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.DTO.UserDto;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<ResponseContract<UserDto>> Create([FromBody] UserToCreateDto dto)
            => (await mediator.Send(dto.Adapt<CreateUserCommand>()))
                .Adapt<ResponseContract<UserDto>>();

        [HttpGet("{id}")]
        public async Task<ResponseContract<UserDto>> GetById([FromRoute] Guid id)
            => (await mediator.Send(new GetUserByIdQuery { Id = id }))
                .Adapt<ResponseContract<UserDto>>();

        [HttpGet]
        public async Task<ResponseContract<List<UserDto>>> GetAll([FromQuery] string? nicknameFilter = null)
            => (await mediator.Send(new GetUsersQuery { NicknameFilter = nicknameFilter }))
                .Adapt<ResponseContract<List<UserDto>>>();

        [HttpPut("{id}/ban")]
        public async Task Ban([FromRoute] Guid id)
            => await mediator.Send(new BanUserCommand { Id = id });

        [HttpPut("{id}/mute")]
        public async Task Mute([FromRoute] Guid id)
            => await mediator.Send(new MuteUserCommand { Id = id });
    }
}