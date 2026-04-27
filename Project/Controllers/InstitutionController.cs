using Application.Behaviour.Institution;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.DTO.Institution;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/institutions")]
    public class InstitutionController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<Guid> Create(CreateInstitutionDto dto)
            => await mediator.Send(new CreateInstitutionCommand
            {
                Name = dto.Name,
                Longitude = dto.Longitude,
                Latitude = dto.Latitude,
                Address = dto.Address,
                Type = dto.Type,
                AveragePrice = dto.AveragePrice,
                WebUrl = dto.WebUrl
            });

        [HttpGet]
        public async Task<List<InstitutionEntity>> GetAll()
            => await mediator.Send(new GetInstitutionQuery());

        [HttpGet("{id}")]
        public async Task<InstitutionEntity> GetById(Guid id)
            => await mediator.Send(new GetInstitutionByIdCommand { Id = id });

        [HttpGet("owner/{ownerId}")]
        public async Task<List<InstitutionEntity>> GetByOwnerId(Guid ownerId)
            => await mediator.Send(new GetInstitutionByOwnerIdQuery { OwnerId = ownerId });

        [HttpPut]
        public async Task Update(UpdateInstitutionDto dto)
            => await mediator.Send(new UpdateInstitutionCommand
            {
                Id = dto.Id,
                Name = dto.Name,
                Address = dto.Address,
                Type = dto.Type,
                AveragePrice = dto.AveragePrice,
                WebUrl = dto.WebUrl
            });

        [HttpPut("assign-owner")]
        public async Task AssignOwner(AssignOwnerDto dto)
            => await mediator.Send(new AssignOwnerToInstitutionCommand
            {
                InstitutionId = dto.InstitutionId,
                OwnerId = dto.OwnerId
            });

        [HttpDelete("{id}")]
        public async Task Delete(Guid id, [FromQuery] bool isArchive = false)
            => await mediator.Send(new DeleteInstitutionCommand { Id = id, IsArchive = isArchive });
    }
}