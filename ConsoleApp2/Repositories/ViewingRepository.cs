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

        public Task<List<ViewingEntity>> GetByInstitutionIdAsync(Guid institutionId)
        {
            return context.Viewings.Where(x => x.InstitutionId == institutionId).ToListAsync();
        }

        public Task DeleteAsync(Guid id)
        {
            return context.Viewings.Where(x => x.Id == id).ExecuteDeleteAsync();
        }
    }
}