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

        public Task<List<WorkingHoursEntity>> GetByInstitutionIdAsync(Guid institutionId)
        {
            return context.WorkingHours.Where(x => x.InstitutionId == institutionId).ToListAsync();
        }

        public Task DeleteAsync(Guid institutionId, DayOfWeek dayOfWeek)
        {
            return context.WorkingHours.Where(x => x.InstitutionId == institutionId && x.DayOfWeek == dayOfWeek).ExecuteDeleteAsync();
        }
    }
}