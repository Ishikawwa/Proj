using Application.Behaviour.ReviewReport;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.DTO;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/spam-reports")]
    public class SpamReportController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<Guid> Create(CreateReviewReportDto dto)
            => await mediator.Send(new CreateReviewReportCommand
            {
                UserId = dto.UserId,
                ReviewId = dto.ReviewId,
                Comment = dto.Comment
            });

        [HttpGet]
        public async Task<List<SpamReportEntity>> GetAll()
            => await mediator.Send(new GetReviewReportQuery());
    }
}