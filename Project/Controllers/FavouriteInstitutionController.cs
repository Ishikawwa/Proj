using Application.Behaviour.Institution;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.DTO.Favorite;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/favourites")]
    public class FavouriteController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<Guid> Add(AddInFavouriteListDto dto)
            => await mediator.Send(new AddInFavoriteListCommand
            {
                UserId = dto.UserId,
                InstitutionId = dto.InstitutionId
            });

        [HttpGet("{userId}")]
        public async Task<List<FavouriteInstitutionEntity>> GetByUser(Guid userId)
            => await mediator.Send(new GetFavoriteListQuery { UserId = userId });
    }
}