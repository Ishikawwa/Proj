using Application.Behaviour.Visiting;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.DTO;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/visitings")]
    public class VisitingController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<Guid> Create(CreateVisitingDto dto)
            => await mediator.Send(new CreateVisitingInstitutionCommand
            {
                UserId = dto.UserId,
                InstitutionId = dto.InstitutionId
            });
    }
}