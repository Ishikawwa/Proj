using Application.Behaviour.Review;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.DTO.ReviewDto;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/reviews")]
    public class ReviewController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<ReviewDto> Create([FromBody] ReviewToCreateDto dto)
            => (await mediator.Send(dto.Adapt<CreateReviewCommand>()))
                .Adapt<ReviewDto>();

        [HttpGet("institution/{institutionId}")]
        public async Task<List<ReviewDto>> GetByInstitution([FromRoute] Guid institutionId)
            => (await mediator.Send(new GetReviewsByInstitutionIdQuery { InstitutionId = institutionId }))
                .Adapt<List<ReviewDto>>();

        [HttpPut]
        public async Task Update([FromBody] ReviewToUpdateDto dto)
            => await mediator.Send(dto.Adapt<UpdateReviewCommand>());

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute] Guid id)
            => await mediator.Send(new DeleteReviewCommand { Id = id });

        [HttpPut("{id}/ban")]
        public async Task Ban([FromRoute] Guid id)
            => await mediator.Send(new BanReviewCommand { Id = id });

        [HttpPost("{reviewId}/vote")]
        public async Task<Guid> VoteOnReview([FromRoute] Guid reviewId, [FromBody] ReviewScoreDto dto)
            => (await mediator.Send(dto.Adapt<ScoreReviewCommand>() with { ReviewId = reviewId })).Id;
    }
}