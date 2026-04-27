using Application.Behaviour.OwnerRequest;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.DTO;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/owner-requests")]
    public class OwnerRequestController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<Guid> Create(CreateOwnerRequestDto dto)
            => await mediator.Send(new CreateOwnerRequestCommand
            {
                UserId = dto.UserId,
                InstitutionId = dto.InstitutionId,
                Comment = dto.Comment
            });

        [HttpGet]
        public async Task<List<OwnerRequestEntity>> GetAll()
            => await mediator.Send(new GetOwnerRequestsQuery());
    }
}