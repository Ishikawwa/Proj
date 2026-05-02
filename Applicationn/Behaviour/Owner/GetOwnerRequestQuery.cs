using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Behaviour.OwnerRequest
{
    public record GetOwnerRequestsQuery : IRequest<List<OwnerRequestEntity>>
    {
    }

    public sealed class GetOwnerRequestsQueryHandler(IOwnerRequestRepository repository) : IRequestHandler<GetOwnerRequestsQuery, List<OwnerRequestEntity>>
    {
        public async Task<List<OwnerRequestEntity>> Handle(GetOwnerRequestsQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetPendingAsync();
        }
    }
}