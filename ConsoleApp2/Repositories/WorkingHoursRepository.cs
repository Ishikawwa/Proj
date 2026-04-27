using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class WorkingHoursRepository(ApplicationContext context) : IWorkingHoursRepository
    {
        public async Task AddAsync(WorkingHoursEntity entity)
        {
            await context.WorkingHours.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task<List<WorkingHoursEntity>> GetByInstitutionIdAsync(Guid institutionId)
        {
            return await context.WorkingHours.Where(x => x.InstitutionId == institutionId).ToListAsync();
        }

        public async Task DeleteAsync(Guid institutionId, DayOfWeek dayOfWeek)
        {
            List<WorkingHoursEntity> entities = await context.WorkingHours
                .Where(x => x.InstitutionId == institutionId).ToListAsync();

            if (entities.Any())
            {
                context.WorkingHours.RemoveRange(entities);
                await context.SaveChangesAsync();
            }
        }
    }
}