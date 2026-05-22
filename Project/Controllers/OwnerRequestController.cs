using Application.Behaviour.OwnerRequest;
using Application.Utils;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.DTO.OwnerDto;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/owner-requests")]
    public class OwnerRequestController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<ResponseContract<OwnerRequestDto>> Create([FromBody] OwnerRequestToCreateDto dto)
            => (await mediator.Send(dto.Adapt<CreateOwnerRequestCommand>()))
                .Adapt<ResponseContract<OwnerRequestDto>>();

        [HttpGet]
        public async Task<ResponseContract<List<OwnerRequestDto>>> GetAll()
            => (await mediator.Send(new GetOwnerRequestsQuery()))
                .Adapt<ResponseContract<List<OwnerRequestDto>>>();

        [HttpPut("{id}/process")]
        public async Task<ResponseContract<Unit>> MarkAsProcessed([FromRoute] Guid id)
            => await mediator.Send(new ProcessOwnerRequestCommand { Id = id });
    }
}