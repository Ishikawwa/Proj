using Application.Behaviour.User;
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
        public async Task<UserDto> Create([FromBody] UserToCreateDto dto)
            => (await mediator.Send(dto.Adapt<CreateUserCommand>()))
                .Adapt<UserDto>();

        [HttpGet("{id}")]
        public async Task<UserDto> GetById([FromRoute] Guid id)
            => (await mediator.Send(new GetUserByIdQuery { Id = id }))
                .Adapt<UserDto>();

        [HttpGet]
        public async Task<List<UserDto>> GetAll([FromQuery] string? nicknameFilter = null)
            => (await mediator.Send(new GetUsersQuery { NicknameFilter = nicknameFilter }))
                .Adapt<List<UserDto>>();

        [HttpPut("{id}/ban")]
        public async Task Ban([FromRoute] Guid id)
            => await mediator.Send(new BanUserCommand { Id = id });

        [HttpPut("{id}/mute")]
        public async Task Mute([FromRoute] Guid id)
            => await mediator.Send(new MuteUserCommand { Id = id });
    }
}