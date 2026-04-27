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

        public async Task<List<OwnerRequestEntity>> GetAllAsync()
        {
            return await context.OwnerRequests.ToListAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            OwnerRequestEntity entity = await context.OwnerRequests.FirstOrDefaultAsync(x => x.Id == id);

            if (entity != null)
            {
                context.OwnerRequests.Remove(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}