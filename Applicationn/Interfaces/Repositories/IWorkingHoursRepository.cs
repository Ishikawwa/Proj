using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IWorkingHoursRepository
    {
        Task AddAsync(WorkingHoursEntity entity);
        Task<List<WorkingHoursEntity>> GetByInstitutionIdAsync(Guid institutionId);
        Task DeleteAsync(Guid institutionId, DayOfWeek dayOfWeek);
    }
}
