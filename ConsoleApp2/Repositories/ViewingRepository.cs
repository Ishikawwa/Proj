using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class ViewingRepository(ApplicationContext context) : IViewingRepository
    {
        public async Task AddAsync(ViewingEntity entity)
        {
            await context.Viewings.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task<List<ViewingEntity>> GetByInstitutionIdAsync(Guid institutionId)
        {
            return await context.Viewings.Where(x => x.InstitutionId == institutionId).ToListAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            ViewingEntity entity = await context.Viewings.FirstOrDefaultAsync(x => x.Id == id);

            if (entity != null)
            {
                context.Viewings.Remove(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}