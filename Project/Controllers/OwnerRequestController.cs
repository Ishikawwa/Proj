using Application.Behaviour.OwnerRequest;
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
        public async Task<OwnerRequestDto> Create([FromBody] OwnerRequestToCreateDto dto)
            => (await mediator.Send(dto.Adapt<CreateOwnerRequestCommand>()))
                .Adapt<OwnerRequestDto>();

        [HttpGet]
        public async Task<List<OwnerRequestDto>> GetAll()
            => (await mediator.Send(new GetOwnerRequestsQuery()))
                .Adapt<List<OwnerRequestDto>>();

        [HttpPut("{id}/process")]
        public async Task MarkAsProcessed([FromRoute] Guid id)
            => await mediator.Send(new ProcessOwnerRequestCommand { Id = id });
    }
}