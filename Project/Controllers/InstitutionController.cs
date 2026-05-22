using Application.Behaviour.Institution;
using Application.Utils;
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
        public async Task<ResponseContract<InstitutionDto>> Create([FromBody] InstitutionToCreateDto dto)
            => (await mediator.Send(dto.Adapt<CreateInstitutionCommand>())).Adapt<ResponseContract<InstitutionDto>>();

        [HttpGet]
        public async Task<ResponseContract<List<InstitutionDto>>> GetAll()
            => (await mediator.Send(new GetInstitutionQuery())).Adapt<ResponseContract<List<InstitutionDto>>>();

        [HttpGet("{id}")]
        public async Task<ResponseContract<InstitutionDto>> GetById([FromRoute] Guid id)
            => (await mediator.Send(new GetInstitutionByIdCommand { Id = id })).Adapt<ResponseContract<InstitutionDto>>();

        [HttpGet("owner/{ownerId}")]
        public async Task<ResponseContract<List<InstitutionDto>>> GetByOwnerId([FromRoute] Guid ownerId)
            => (await mediator.Send(new GetInstitutionByOwnerIdQuery { OwnerId = ownerId }))
                .Adapt<ResponseContract<List<InstitutionDto>>>();

        [HttpPut]
        public async Task<ResponseContract<Unit>> Update([FromBody] InstitutionToUpdateDto dto)
            => await mediator.Send(dto.Adapt<UpdateInstitutionCommand>());

        [HttpPut("assign-owner")]
        public async Task<ResponseContract<Unit>> AssignOwner([FromBody] AssignOwnerToInstitutionDto dto)
            => await mediator.Send(dto.Adapt<AssignOwnerToInstitutionCommand>());

        [HttpDelete("{id}")]
        public async Task<ResponseContract<Unit>> Delete([FromRoute] Guid id)
            => await mediator.Send(new DeleteInstitutionCommand { Id = id });

        [HttpPost("{institutionId}/labels")]
        public async Task<ResponseContract<Unit>> AddLabel([FromRoute] Guid institutionId, [FromBody] InstitutionLabelToManageDto dto)
            => await mediator.Send(dto.Adapt<AddLabelToInstitutionCommand>() with { InstitutionId = institutionId });

        [HttpDelete("{institutionId}/labels/{label}")]
        public async Task<ResponseContract<Unit>> RemoveLabel([FromRoute] Guid institutionId, [FromRoute] LabelTypeEnum label)
            => await mediator.Send(new RemoveLabelFromInstitutionCommand { InstitutionId = institutionId, Label = label });

    }
}