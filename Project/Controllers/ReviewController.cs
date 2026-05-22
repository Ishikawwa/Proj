using Application.Behaviour.Review;
using Application.Utils;
using Domain.Entities;
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
        public async Task<ResponseContract<ReviewDto>> Create([FromBody] ReviewToCreateDto dto)
            => (await mediator.Send(dto.Adapt<CreateReviewCommand>()))
                .Adapt<ResponseContract<ReviewDto>>();

        [HttpGet("institution/{institutionId}")]
        public async Task<ResponseContract<List<ReviewDto>>> GetByInstitution([FromRoute] Guid institutionId)
            => (await mediator.Send(new GetReviewsByInstitutionIdQuery { InstitutionId = institutionId }))
                .Adapt<ResponseContract<List<ReviewDto>>>();

        [HttpPut]
        public async Task<ResponseContract<Unit>> Update([FromBody] ReviewToUpdateDto dto)
            => await mediator.Send(dto.Adapt<UpdateReviewCommand>());

        [HttpDelete("{id}")]
        public async Task<ResponseContract<Unit>> Delete([FromRoute] Guid id)
            => await mediator.Send(new DeleteReviewCommand { Id = id });

        [HttpPut("{id}/ban")]
        public async Task<ResponseContract<Unit>> Ban([FromRoute] Guid id)
            => await mediator.Send(new BanReviewCommand { Id = id });

        [HttpPost("{reviewId}/vote")]
        public async Task<ResponseContract<ReviewScoreEntity>> VoteOnReview([FromRoute] Guid reviewId, [FromBody] ReviewScoreDto dto)
            => await mediator.Send(dto.Adapt<ScoreReviewCommand>() with { ReviewId = reviewId });
    }
}