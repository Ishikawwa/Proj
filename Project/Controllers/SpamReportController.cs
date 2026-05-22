using Application.Behaviour.ReviewReport;
using Application.Utils;
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
        public async Task<ResponseContract<CreateSpamReportDto>> Create([FromBody] CreateReviewReportDto dto)
            => (await mediator.Send(dto.Adapt<CreateReviewReportCommand>()))
                .Adapt<ResponseContract<CreateSpamReportDto>>();

        [HttpGet]
        public async Task<ResponseContract<List<CreateSpamReportDto>>> GetAll()
            => (await mediator.Send(new GetReviewReportQuery()))
                .Adapt<ResponseContract<List<CreateSpamReportDto>>>();

        [HttpPut("{id}/process")]
        public async Task<ResponseContract<Unit>> MarkAsProcessed([FromRoute] Guid id)
            => await mediator.Send(new ProcessSpamReportCommand { Id = id });
    }
}