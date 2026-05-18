using Application.Behaviour.Visiting;
using Application.Utils;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.DTO.UserVisitingDto;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/visitings")]
    public class VisitingController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<ResponseContract<VisitingDto>> Create([FromBody] CreateVisitingDto dto)
            => (await mediator.Send(dto.Adapt<CreateVisitingInstitutionCommand>()))
                .Adapt<ResponseContract<VisitingDto>>();
    }
}