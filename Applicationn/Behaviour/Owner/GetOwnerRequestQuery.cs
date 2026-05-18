using Application.Interfaces.Repositories;
using Application.Utils;
using Domain.Entities;
using MediatR;

namespace Application.Behaviour.OwnerRequest
{
    public record GetOwnerRequestsQuery : IRequest<ResponseContract<List<OwnerRequestEntity>>>
    {
    }

    public sealed class GetOwnerRequestsQueryHandler(IOwnerRequestRepository repository) : IRequestHandler<GetOwnerRequestsQuery, ResponseContract<List<OwnerRequestEntity>>>
    {
        public async Task<ResponseContract<List<OwnerRequestEntity>>> Handle(GetOwnerRequestsQuery request, CancellationToken cancellationToken)
        {
            await repository.GetPendingAsync();

            return new ResponseContract<List<OwnerRequestEntity>>();
        }
    }
}