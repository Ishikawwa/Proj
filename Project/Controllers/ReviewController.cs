using Application.Behaviour.Review;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.DTO;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/reviews")]
    public class ReviewController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<Guid> Create(CreateReviewDto dto)
            => await mediator.Send(new CreateReviewCommand
            {
                UserId = dto.UserId,
                InstitutionId = dto.InstitutionId,
                Comment = dto.Comment,
                Score = dto.Score
            });

        [HttpGet("institution/{institutionId}")]
        public async Task<List<ReviewEntity>> GetByInstitution(Guid institutionId)
            => await mediator.Send(new GetReviewsByInstitutionIdQuery { InstitutionId = institutionId });

        [HttpPut]
        public async Task Update(UpdateReviewDto dto)
            => await mediator.Send(new UpdateReviewCommand
            {
                Id = dto.Id,
                Comment = dto.Comment,
                Score = dto.Score
            });

        [HttpDelete("{id}")]
        public async Task Delete(Guid id, [FromQuery] bool isArchive = false)
            => await mediator.Send(new DeleteReviewCommand { Id = id, IsArchive = isArchive });

        [HttpPut("{id}/ban")]
        public async Task Ban(Guid id, [FromQuery] bool isBanned= true)
            => await mediator.Send(new BanReviewCommand { Id = id, IsBanned = isBanned});

        [HttpPost("{reviewId}/score")]
        public async Task<Guid> Score(Guid reviewId, ScoreReviewDto dto)
            => await mediator.Send(new ScoreReviewCommand
            {
                ReviewId = reviewId,
                UserId = dto.UserId,
                IsLiked = dto.IsLiked
            });
    }
}