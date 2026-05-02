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

        public Task<List<InstitutionLabelEntity>> GetByInstitutionIdAsync(Guid institutionId)
        {
            return context.InstitutionLabels.Where(x => x.InstitutionId == institutionId).ToListAsync();
        }

        public Task DeleteAsync(Guid institutionId)
        {
            return context.InstitutionLabels.Where(x => x.InstitutionId == institutionId).ExecuteDeleteAsync();
        }
    }
}