using Application.Behaviour.ReviewReport;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.DTO.ReportDto;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/spam-reports")]
    public class SpamReportController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<CreateSpamReportDto> Create([FromBody] CreateReviewReportDto dto)
            => (await mediator.Send(dto.Adapt<CreateReviewReportCommand>())).Adapt<CreateSpamReportDto>();

        [HttpGet]
        public async Task<List<CreateSpamReportDto>> GetAll()
            => (await mediator.Send(new GetReviewReportQuery())).Adapt<List<CreateSpamReportDto>>();

        [HttpPut("{id}/process")]
        public async Task MarkAsProcessed([FromRoute] Guid id)
            => await mediator.Send(new ProcessSpamReportCommand { Id = id });
    }
}