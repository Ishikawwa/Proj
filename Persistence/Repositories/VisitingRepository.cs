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

        public Task<List<VisitingEntity>> GetByInstitutionIdAsync(Guid institutionId)
        {
            return context.Visitings.Where(x => x.InstitutionId == institutionId).ToListAsync();
        }

        public Task DeleteAsync(Guid id)
        {
            return context.Visitings.Where(x => x.Id == id).ExecuteDeleteAsync();
        }
    }
}