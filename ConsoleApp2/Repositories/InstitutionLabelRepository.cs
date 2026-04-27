using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class InstitutionLabelRepository(ApplicationContext context) : IInstitutionLabelRepository
    {
        public async Task AddAsync(InstitutionLabelEntity entity)
        {
            await context.InstitutionLabels.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task<List<InstitutionLabelEntity>> GetByInstitutionIdAsync(Guid institutionId)
        {
            return await context.InstitutionLabels.Where(x => x.InstitutionId == institutionId).ToListAsync();
        }

        public async Task DeleteAsync(Guid institutionId)
        {
            List<InstitutionLabelEntity> entities = await context.InstitutionLabels.Where(x => x.InstitutionId == institutionId).ToListAsync();

            if (entities.Any())
            {
                context.InstitutionLabels.RemoveRange(entities);
                await context.SaveChangesAsync();
            }
        }
    }
}