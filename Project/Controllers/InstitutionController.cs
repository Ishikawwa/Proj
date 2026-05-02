using Application.Behaviour.Institution;
using Domain.Enums;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.DTO.InstitutionDto;
using Project.DTO.OwnerDto;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/institutions")]
    public class InstitutionController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<InstitutionDto> Create([FromBody] InstitutionToCreateDto dto)
            => (await mediator.Send(dto.Adapt<CreateInstitutionCommand>())).Adapt<InstitutionDto>();

        [HttpGet]
        public async Task<List<InstitutionDto>> GetAll()
            => (await mediator.Send(new GetInstitutionQuery())).Adapt<List<InstitutionDto>>();

        [HttpGet("{id}")]
        public async Task<InstitutionDto> GetById([FromRoute] Guid id)
            => (await mediator.Send(new GetInstitutionByIdCommand { Id = id })).Adapt<InstitutionDto>();

        [HttpGet("owner/{ownerId}")]
        public async Task<List<InstitutionDto>> GetByOwnerId([FromRoute] Guid ownerId)
            => (await mediator.Send(new GetInstitutionByOwnerIdQuery { OwnerId = ownerId }))
                .Adapt<List<InstitutionDto>>();

        [HttpPut]
        public async Task Update([FromBody] InstitutionToUpdateDto dto)
            => await mediator.Send(dto.Adapt<UpdateInstitutionCommand>());

        [HttpPut("assign-owner")]
        public async Task AssignOwner([FromBody] AssignOwnerToInstitutionDto dto)
            => await mediator.Send(dto.Adapt<AssignOwnerToInstitutionCommand>());

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute] Guid id)
            => await mediator.Send(new DeleteInstitutionCommand { Id = id });

        [HttpPost("{institutionId}/labels")]
        public async Task AddLabel([FromRoute] Guid institutionId, [FromBody] InstitutionLabelToManageDto dto)
            => await mediator.Send(dto.Adapt<AddLabelToInstitutionCommand>() with { InstitutionId = institutionId });

        [HttpDelete("{institutionId}/labels/{label}")]
        public async Task RemoveLabel([FromRoute] Guid institutionId, [FromRoute] LabelTypeEnum label)
            => await mediator.Send(new RemoveLabelFromInstitutionCommand { InstitutionId = institutionId, Label = label });

    }
}