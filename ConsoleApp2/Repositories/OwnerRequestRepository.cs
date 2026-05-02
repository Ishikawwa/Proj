using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class OwnerRequestRepository(ApplicationContext context) : IOwnerRequestRepository
    {
        public async Task AddAsync(OwnerRequestEntity entity)
        {
            await context.OwnerRequests.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public Task DeleteAsync(Guid id)
        {
            return context.OwnerRequests.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public Task<List<OwnerRequestEntity>> GetPendingAsync()
        {
            return context.OwnerRequests.Where(x => !x.IsProcessed).ToListAsync();
        }

        public Task MarkAsProcessedAsync(Guid id)
        {
            return context.OwnerRequests.Where(x => x.Id == id).ExecuteUpdateAsync(s => s.SetProperty(x => x.IsProcessed, true));
        }
    }
}