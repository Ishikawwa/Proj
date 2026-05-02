using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IOwnerRequestRepository
    {
        Task AddAsync(OwnerRequestEntity entity);
        Task<List<OwnerRequestEntity>> GetPendingAsync();
        Task MarkAsProcessedAsync(Guid id);
        Task DeleteAsync(Guid id);
    }
}
