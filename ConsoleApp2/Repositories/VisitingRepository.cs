using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class VisitingRepository(ApplicationContext context) : IVisitingRepository
    {
        public async Task AddAsync(VisitingEntity entity)
        {
            await context.Visitings.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task<List<VisitingEntity>> GetByInstitutionIdAsync(Guid institutionId)
        {
            return await context.Visitings.Where(x => x.InstitutionId == institutionId).ToListAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            VisitingEntity entity = await context.Visitings.FirstOrDefaultAsync(x => x.Id == id);

            if (entity != null)
            {
                context.Visitings.Remove(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}