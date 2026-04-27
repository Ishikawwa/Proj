using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IWorkingHoursRepository
    {
        Task AddAsync(WorkingHoursEntity entity);
        Task<List<WorkingHoursEntity>> GetByInstitutionIdAsync(Guid institutionId);
        Task DeleteAsync(Guid institutionId, DayOfWeek dayOfWeek);
    }
}
