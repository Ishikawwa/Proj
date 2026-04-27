using Application.Behaviour.User;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.DTO;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<Guid> Create(CreateUserDto dto)
            => await mediator.Send(new CreateUserCommand
            {
                Nickname = dto.Nickname,
                AvatarUrl = dto.AvatarUrl
            });

        [HttpGet("{id}")]
        public async Task<UserEntity> GetById(Guid id)
            => await mediator.Send(new GetUserByIdQuery { Id = id });

        [HttpGet]
        public async Task<List<UserEntity>> GetAll([FromQuery] string? nicknameFilter = null)
            => await mediator.Send(new GetUsersQuery { NicknameFilter = nicknameFilter });

        [HttpPut("{id}/ban")]
        public async Task Ban(Guid id)
            => await mediator.Send(new BanUserCommand { Id = id });

        [HttpPut("{id}/mute")]
        public async Task Mute(Guid id)
            => await mediator.Send(new MuteUserCommand { Id = id });
    }
}