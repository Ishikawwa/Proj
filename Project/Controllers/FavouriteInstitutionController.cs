using Application.Behaviour.Institution;
using Application.Utils;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.DTO.FavoriteInstitutionDto;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/favourites")]
    public class FavouriteController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<ResponseContract<Unit>> Add([FromBody] AddInFavoriteInstitutionListDto dto)
            => await mediator.Send(dto.Adapt<AddInFavoriteListCommand>());

        [HttpGet("{userId}")]
        public async Task<ResponseContract<List<UsersFavoriteInstitutionDto>>> GetByUser([FromRoute] Guid userId)
            => (await mediator.Send(new GetFavoriteListQuery { UserId = userId }))
                .Adapt<ResponseContract<List<UsersFavoriteInstitutionDto>>>();

        [HttpDelete("{id}")]
        public async Task<ResponseContract<Unit>> Remove([FromRoute] Guid id)
            => await mediator.Send(new RemoveFromFavoriteListCommand { Id = id });
    }
}